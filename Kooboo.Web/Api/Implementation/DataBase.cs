//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.Api;
using Kooboo.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Web.Api.Implementation
{
    public class DataBaseApi : IApi
    {
        public string ModelName
        {
            get { return "Database"; }
        }

        public bool RequireSite
        {
            get { return true; }
        }

        public bool RequireUser
        {
            get { return true; }
        }

        public List<string> Tables(ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);
            var list =  db.GetTables();

            list.RemoveAll(o => o.StartsWith("_sys_"));

            list.RemoveAll(o => o.StartsWith("_koobootemp")); 

            return list; 
        }

        [Attributes.RequireParameters("table")]
        public PagedListViewModel<IDictionary<string, object>> Data(ApiCall call)
        { 
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite); 
            var table = db.GetOrCreateTable(call.GetValue("table"));
             
            var total =  table.All();

            var pager = ApiHelper.GetPager(call, 30);

            PagedListViewModel<IDictionary<string, object>> result = new PagedListViewModel<IDictionary<string, object>>(); 

            result.TotalCount = total.Count;
            result.TotalPages = ApiHelper.GetPageCount(total.Count, pager.PageSize);
            result.PageNr = pager.PageNr;
            result.PageSize = pager.PageSize;

            int totalskip = 0;
            if (pager.PageNr > 1)
            {
                totalskip = (pager.PageNr - 1) * pager.PageSize;
            }

            var items = total.Skip(totalskip).Take(pager.PageSize).ToList();  

            if (items != null && items.Count() > 0)
            {
                result.List = items; 
            }

            return result; 
        }
          
        public void CreateTable(string name, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            if (!Kooboo.IndexedDB.Helper.CharHelper.IsValidTableName(name))
            {
                throw new Exception(Kooboo.Data.Language.Hardcoded.GetValue("Only Alphanumeric are allowed to use as a table", call.Context)); 
            }
            var table = db.GetOrCreateTable(name);

            return;
        }
         
        [Attributes.RequireParameters("names")]
        public void DeleteTables(ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            string json = call.GetValue("names");
            if (string.IsNullOrEmpty(json))
            {
                json = call.Context.Request.Body;
            }
            List<string> ids = Lib.Helper.JsonHelper.Deserialize<List<string>>(json);

            if (ids != null && ids.Count() > 0)
            {
                foreach (var item in ids)
                {
                    db.DeleteTable(item);
                }
            }
        }

   
        public bool IsUniqueTableName(string name, ApiCall call)
        {
            name = name.ToLower(); 

            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            var tables = db.GetTables();

            foreach (var item in tables)
            {
                 if (item.ToLower() == name)
                {
                    return false; 
                }
            }   
            return true;
        }

        public List<string> AvailableControlTypes(ApiCall call)
        {
            return Kooboo.Sites.Contents.Models.ControlTypes.List;
        }

        public List<DatabaseColumnViewModel> Columns(string table, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            var dbTable = db.GetOrCreateTable(table);

            List<DatabaseColumnViewModel> result = new List<DatabaseColumnViewModel>();

            foreach (var item in dbTable.Setting.Columns)
            {
                DatabaseColumnViewModel model = new DatabaseColumnViewModel() { Name = item.Name, IsIncremental = item.IsIncremental, IsUnique = item.IsUnique, IsIndex = item.IsIndex, IsPrimaryKey = item.IsPrimaryKey, Seed = item.Seed, Scale = item.Increment, IsSystem = item.IsSystem };

                model.DataType = DatabaseColumnHelper.ToFrontEndDataType(item.ClrType);

                model.ControlType = item.ControlType;
                model.Setting = item.Setting;

                result.Add(model);
            }
            return result;
        } 

        public List<DatabaseItemEdit> GetEdit(string tablename, string Id, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            var dbTable = db.GetOrCreateTable(tablename);

            List<DatabaseItemEdit> result = new List<DatabaseItemEdit>();

            var obj = dbTable.Get(Id);

            foreach (var item in dbTable.Setting.Columns)
            {
                DatabaseItemEdit model = new DatabaseItemEdit() { Name = item.Name, IsIncremental = item.IsIncremental, IsUnique = item.IsUnique, IsIndex = item.IsIndex, IsPrimaryKey = item.IsPrimaryKey, Seed = item.Seed, Scale = item.Increment, IsSystem = item.IsSystem };

                model.DataType = DatabaseColumnHelper.ToFrontEndDataType(item.ClrType);

                model.ControlType = item.ControlType;
                model.Setting = item.Setting;

                // get value
                if (obj != null && obj.ContainsKey(model.Name))
                {

                    model.Value = obj[model.Name];
                }

                result.Add(model);
            }
            return result;
        }


        public Guid UpdateData(string tablename, Guid id, List<DatabaseItemEdit> Values, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            var dbTable = db.GetOrCreateTable(tablename);

            List<DatabaseItemEdit> result = new List<DatabaseItemEdit>();

            if (id != default(Guid))
            {
                var obj = dbTable.Get(id);
                if (obj == null)
                {
                    return default(Guid);
                }

                foreach (var item in dbTable.Setting.Columns.Where(o=>!o.IsSystem))
                {
                    var value = Values.Find(o => o.Name.ToLower() == item.Name.ToLower());
                    if (value == null)
                    {
                        obj.Remove(item.Name);
                    }
                    else
                    {
                        var rightvalue = Lib.Reflection.TypeHelper.ChangeType(value.Value, item.ClrType);
                        obj[item.Name] = rightvalue;
                    }
                }
                dbTable.Update(id, obj);
            }
            else
            {
                var obj = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                 
                foreach (var item in dbTable.Setting.Columns.Where(o=>!o.IsSystem))
                { 
                    if (!item.IsIncremental)
                    {
                        var value = Values.Find(o => o.Name.ToLower() == item.Name.ToLower());
                        if (value == null)
                        {
                            obj.Remove(item.Name);
                        }
                        else
                        {
                            var rightvalue = Lib.Reflection.TypeHelper.ChangeType(value.Value, item.ClrType);
                            obj[item.Name] = rightvalue;
                        }
                    } 
                }
                return dbTable.Add(obj);     
            }

            return default(Guid); 
        }

        public void DeleteData(string tablename, List<Guid> values, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);

            var dbTable = db.GetOrCreateTable(tablename);

            foreach (var item in values)
            {
                dbTable.Delete(item); 
            }
        }

        public void UpdateColumn(string tablename, List<DatabaseColumnViewModel> columns, ApiCall call)
        {
            var db = Kooboo.Data.DB.GetKDatabase(call.Context.WebSite);
            var table = db.GetOrCreateTable(tablename);

            var setting = Lib.Serializer.Copy.DeepCopy<IndexedDB.Dynamic.Setting>(table.Setting);

            // deleted items. 
            setting.Columns.RemoveWhere(o => columns.Find(m => o.Name.ToLower() == m.Name.ToLower()) == null && o.Name != IndexedDB.Dynamic.Constants.DefaultIdFieldName);

            // update items or new added items. 

            foreach (var item in columns)
            {
                if (item.Name == Kooboo.IndexedDB.Dynamic.Constants.DefaultIdFieldName)
                {
                    continue;
                }

                var find = setting.Columns.FirstOrDefault(o => o.Name.ToLower() == item.Name.ToLower());
                if (find == null)
                {
                    Type datatype = DatabaseColumnHelper.ToClrType(item.DataType);
                    setting.AppendColumn(item.Name, datatype, 0);
                    var col = setting.Columns.FirstOrDefault(o => o.Name == item.Name);
                    col.Setting = item.Setting;
                    col.ControlType = item.ControlType;
                    col.IsIncremental = item.IsIncremental;
                    col.Seed = item.Seed;
                    col.Increment = item.Scale;
                    col.IsIndex = item.IsIndex;
                    col.IsPrimaryKey = item.IsPrimaryKey;
                    col.IsUnique = item.IsUnique;
                }
                else
                {
                    find.Setting = item.Setting;
                    find.ControlType = item.ControlType;
                    find.IsIncremental = item.IsIncremental;
                    find.Seed = item.Seed;
                    find.Increment = item.Scale;
                    find.IsIndex = item.IsIndex;
                    find.IsPrimaryKey = item.IsPrimaryKey;
                    find.IsUnique = item.IsUnique;
                }
            }
            
            setting.EnsurePrimaryKey(""); 
            
            table.UpdateSetting(setting);

            table.Close();
           
        }

    }

    public static class DatabaseColumnHelper
    {
        public static string ToFrontEndDataType(Type clrType)
        {
            // string, number, datetime, bool, undefined 
            if (clrType == typeof(Int16) || clrType == typeof(Int32) || clrType == typeof(Int64) || clrType == typeof(byte))
            {
                return "number";
            }
            else if (clrType == typeof(decimal) || clrType == typeof(double) || clrType == typeof(float))
            {
                return "number";
            }
            else if (clrType == typeof(DateTime))
            {
                return "datetime";
            }
            else if (clrType == typeof(bool))
            {
                return "bool";
            }
            else if (clrType == typeof(string) || clrType == typeof(Guid))
            {
                return "string";
            }
            return "string";
        }

        public static Type ToClrType(string type)
        {
            string lower = type.ToLower();

            if (lower == "datetime")
            {
                return typeof(System.DateTime);
            }
            else if (lower == "number")
            {
                return typeof(System.Double);
            }
            else if (lower == "bool")
            {
                return typeof(bool);
            }
            else if (lower == "string")
            {
                return typeof(string);
            }
            return typeof(string);
        }
    }
}