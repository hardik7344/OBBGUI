using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TectonaDatabaseHandlerDLL;

namespace OBB_Project.DataAccessLayer
{
    public class QueryHandler
    {
        OwnYITConstant.DatabaseTypes dbtype;
        private DataTable data_table = new DataTable();
        DatabaseCommon objcommon = new DatabaseCommon();

        public QueryHandler()
        {
            switch (OwnYITConstant.DBType)
            {
                case 0:
                    dbtype = OwnYITConstant.DatabaseTypes.MSSQL_SERVER;
                    break;
                case 1:
                    dbtype = OwnYITConstant.DatabaseTypes.MYSQL_SERVER;
                    break;
                case 2:
                    dbtype = OwnYITConstant.DatabaseTypes.PGSQL_SERVER;
                    break;
                case 3:
                    dbtype = OwnYITConstant.DatabaseTypes.CASSANDRA_SERVER;
                    break;
                //case 4:
                //    dbtype = OwnYITConstant.DatabaseTypes.MARIADB_SERVER;
                //    break;
            }

           
        }



        #region login

        public string doUserLogin(string username, string password)
        {
            StringBuilder strQueryBuilder = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQueryBuilder.AppendFormat("select count(*) from entity_master em, entity_adhoc_info eai where em.entity_id = eai.entity_id and ");
                        strQueryBuilder.AppendFormat("em.entity_type_id = 1 and em.entity_name ='{0}' and eai.entity_value ='{1}' and eai.active = 1 and eai.product_type = 1", username, password);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQueryBuilder.AppendFormat("select count(*) from entity_master em, entity_adhoc_info eai where em.entity_id = eai.entity_id and ");
                        strQueryBuilder.AppendFormat("em.entity_type_id = 1 and em.entity_name ='{0}' and eai.entity_value ='{1}' and eai.active = 1 and eai.product_type = 1", username, password);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQueryBuilder.AppendFormat("select count(*) from entity_master em, entity_adhoc_info eai where em.entity_id = eai.entity_id and ");
                        strQueryBuilder.AppendFormat("em.entity_type_id = 1 and em.entity_name ='{0}' and eai.entity_value ='{1}' and eai.active = 1 and eai.product_type = 1", username, password);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQueryBuilder.AppendFormat("select count(*) from entity_master em, entity_adhoc_info eai where em.entity_id = eai.entity_id and ");
                    //    strQueryBuilder.AppendFormat("em.entity_type_id = 1 and em.entity_name ='{0}' and eai.entity_value ='{1}' and eai.active = 1 and eai.product_type = 1", username, password);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "doUserLogin Query : " + strQueryBuilder.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQueryBuilder.ToString();
        }

        public string get_user_id(string username)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_id from entity_master where entity_name = '{0}' and entity_type_id = 1 and active = 1", username);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_id from entity_master where entity_name = '{0}' and entity_type_id = 1 and active = 1", username);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_id from entity_master where entity_name = '{0}' and entity_type_id = 1 and active = 1", username);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_id from entity_master where entity_name = '{0}' and entity_type_id = 1 and active = 1", username);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_user_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_Dashbord_chats(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select mm.menu_id,case t.menu_id when isnull(t.menu_id, 0) then 1 else 0 end as menu_active from menu_master mm left outer join ");
                        strQuery.AppendFormat(" (select distinct mms.menu_id from menu_master mms, access_rights_master arm where mms.menu_id = arm.menu_id and arm.user_id = {0} and arm.active = 1) t on mm.menu_id = t.menu_id where mm.product_type = 1 and mm.active = 1 order by mm.menu_id ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select mm.menu_id,case t.menu_id when ifnull(t.menu_id, 0) then 1 else 0 end as menu_active from menu_master mm left outer join ");
                        strQuery.AppendFormat(" (select distinct mms.menu_id from menu_master mms, access_rights_master arm where mms.menu_id = arm.menu_id and arm.user_id = {0} and arm.active = 1) t on mm.menu_id = t.menu_id where mm.product_type = 1 and mm.active = 1 order by mm.menu_id ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select mm.menu_id,case when t.menu_id is null then 0 else 1 end as menu_active from menu_master mm left outer join ");
                        strQuery.AppendFormat(" (select distinct mms.menu_id from menu_master mms, access_rights_master arm where mms.menu_id = arm.menu_id and arm.user_id = {0} and arm.active = 1) t on mm.menu_id = t.menu_id where mm.product_type = 1 and mm.active = 1 order by mm.menu_id ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                        //strQuery.AppendFormat("select mm.menu_id,case t.menu_id when ifnull(t.menu_id, 0) then 1 else 0 end as menu_active from menu_master mm left outer join ");
                        //strQuery.AppendFormat(" (select distinct mms.menu_id from menu_master mms, access_rights_master arm where mms.menu_id = arm.menu_id and arm.user_id = {0} and arm.active = 1) t on mm.menu_id = t.menu_id where mm.product_type = 1 and mm.active = 1 order by mm.menu_id ", user_id);
                        //break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Dashbord_chats Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_menu_url(string menuid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_url from menu_master where menu_id = {0} and active = 1 order by menu_priority asc", menuid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_url from menu_master where menu_id = {0} and active = 1 order by menu_priority asc", menuid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_url from menu_master where menu_id = {0} and active = 1 order by menu_priority asc", menuid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_url from menu_master where menu_id = {0} and active = 1 order by menu_priority asc", menuid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_menu_url Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_menu_data(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_id = {0} and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_id = {0} and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_id = {0} and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_id = {0} and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_menu_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string GetMainMenu(string menuid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_url from menu_master where menu_parent_id=0 and product_type = 1 and active = 1 and menu_id in (" + menuid + ") order by menu_priority asc");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_url from menu_master where menu_parent_id=0 and product_type = 1 and active = 1 and menu_id in (" + menuid + ") order by menu_priority asc");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_url from menu_master where menu_parent_id=0 and product_type = 1 and active = 1 and menu_id in (" + menuid + ") order by menu_priority asc");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_id,menu_name,menu_url from menu_master where menu_parent_id=0 and product_type = 1 and active = 1 and menu_id in (" + menuid + ") order by menu_priority asc");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "GetMainMenu Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region settings organization_structure

        public string Get_treedata()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid from ou_nodelinkage where active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid from ou_nodelinkage where active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid from ou_nodelinkage where active = 1 ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid from ou_nodelinkage where active = 1 ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_treedata Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_Level()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select entity_type_id,entity_instance_id as levelid,entity_instance_name as levelname,'/images/status.png' as img from entity_defination_master where entity_type_id = 0 and active = 1 order by entity_instance_id ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select entity_type_id,entity_instance_id as levelid,entity_instance_name as levelname,'/images/status.png' as img from entity_defination_master where entity_type_id = 0 and active = 1 order by entity_instance_id ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select entity_type_id,entity_instance_id as levelid,entity_instance_name as levelname,'/images/status.png' as img from entity_defination_master where entity_type_id = 0 and active = 1 order by entity_instance_id ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select entity_type_id,entity_instance_id as levelid,entity_instance_name as levelname,'/images/status.png' as img from entity_defination_master where entity_type_id = 0 and active = 1 order by entity_instance_id ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Level Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_defination_master_count(string entity_type_id, string user_type, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_defination_master_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string insert_entity_defination_master(string entity_type_id, string instance_id, string user_type, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_defination_master (entity_type_id,entity_instance_id,entity_instance_name,startdate,enddate,active,product_type) values ({0},'{1}','{2}',getdate(),null,1,{3})", entity_type_id, instance_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_defination_master (entity_type_id,entity_instance_id,entity_instance_name,startdate,enddate,active,product_type) values ({0},'{1}','{2}',now(),null,1,{3})", entity_type_id, instance_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_defination_master (entity_type_id,entity_instance_id,entity_instance_name,startdate,enddate,active,product_type) values ({0},'{1}','{2}',now(),null,1,{3})", entity_type_id, instance_id, user_type, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into entity_defination_master (entity_type_id,entity_instance_id,entity_instance_name,startdate,enddate,active,product_type) values ({0},'{1}','{2}',now(),null,1,{3})", entity_type_id, instance_id, user_type, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_entity_defination_master Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Update_level(string id, string propertyname)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name ='{1}' where entity_instance_id = {0} and entity_type_id = 0 and active = 1 ", id, propertyname);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name ='{1}' where entity_instance_id = {0} and entity_type_id = 0 and active = 1 ", id, propertyname);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name ='{1}' where entity_instance_id = {0} and entity_type_id = 0 and active = 1 ", id, propertyname);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_defination_master set entity_instance_name ='{1}' where entity_instance_id = {0} and entity_type_id = 0 and active = 1 ", id, propertyname);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Update_level Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_level_rights(string levelid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_type_id,edm.entity_instance_id,edm.entity_instance_name,ed.entity_description_name,case when olr.entity_instance_id is null then 0 else 1 end as appliedrights ");
                        strQuery.AppendFormat("from entity_defination_master edm left outer join ou_levelrights olr on edm.entity_instance_id = olr.entity_instance_id and olr.entity_level_id = {0} ", levelid);
                        strQuery.AppendFormat("left outer join entity_description ed on edm.entity_type_id = ed.entity_type_id where edm.entity_instance_id in (1001,1010,1020,2001,3001,3002) and edm.active = 1 order by edm.entity_type_id,edm.entity_instance_id ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_type_id,edm.entity_instance_id,edm.entity_instance_name,ed.entity_description_name,case when olr.entity_instance_id is null then 0 else 1 end as appliedrights ");
                        strQuery.AppendFormat("from entity_defination_master edm left outer join ou_levelrights olr on edm.entity_instance_id = olr.entity_instance_id and olr.entity_level_id = {0} ", levelid);
                        strQuery.AppendFormat("left outer join entity_description ed on edm.entity_type_id = ed.entity_type_id where edm.entity_instance_id in (1001,1010,1020,2001,3001,3002) and edm.active = 1 order by edm.entity_type_id,edm.entity_instance_id ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_type_id,edm.entity_instance_id,edm.entity_instance_name,ed.entity_description_name,case when olr.entity_instance_id is null then 1 else 0 end as appliedrights ");
                        strQuery.AppendFormat("from entity_defination_master edm left outer join ou_levelrights olr on edm.entity_instance_id = olr.entity_instance_id and olr.entity_level_id = {0} ", levelid);
                        strQuery.AppendFormat("left outer join entity_description ed on edm.entity_type_id = ed.entity_type_id where edm.entity_instance_id in (1001,1010,1020,2001,3001,3002) and edm.active = 1 order by edm.entity_type_id,edm.entity_instance_id ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select edm.entity_type_id,edm.entity_instance_id,edm.entity_instance_name,ed.entity_description_name,case when olr.entity_instance_id is null then 0 else 1 end as appliedrights ");
                    //    strQuery.AppendFormat("from entity_defination_master edm left outer join ou_levelrights olr on edm.entity_instance_id = olr.entity_instance_id and olr.entity_level_id = {0} ", levelid);
                    //    strQuery.AppendFormat("left outer join entity_description ed on edm.entity_type_id = ed.entity_type_id where edm.entity_instance_id in (1001,1010,1020,2001,3001,3002) and edm.active = 1 order by edm.entity_type_id,edm.entity_instance_id ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_level_rights Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_levelrights_delete(string levelid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_level_id = {0}", levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_level_id = {0}", levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_level_id = {0}", levelid);
                        break;
                        //    case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                        //        strQuery.AppendFormat("delete from ou_levelrights where entity_level_id = {0}", levelid);
                        //        break;
                        //}
                        //objcommon.WriteLog("DBQueryHandler", "ou_levelrights_delete Query : " + strQuery.ToString());
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_type_id_instance_id(string propertyid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id from entity_defination_master where entity_instance_id in ({0}) and active = 1 ", propertyid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id from entity_defination_master where entity_instance_id in ({0}) and active = 1 ", propertyid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id from entity_defination_master where entity_instance_id in ({0}) and active = 1 ", propertyid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_type_id,entity_instance_id from entity_defination_master where entity_instance_id in ({0}) and active = 1 ", propertyid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_type_id_instance_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_Level_RightsCount(string entity_type_id, string entity_instance_id, string levelid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} and entity_level_id = {2} ", entity_type_id, entity_instance_id, levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} and entity_level_id = {2} ", entity_type_id, entity_instance_id, levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} and entity_level_id = {2} ", entity_type_id, entity_instance_id, levelid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} and entity_level_id = {2} ", entity_type_id, entity_instance_id, levelid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Level_RightsCount Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string insert_level_rights(string entity_type_id, string entity_instance_id, string levelid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_levelrights (entity_type_id,entity_instance_id,entity_level_id) values ({0},{1},{2})", entity_type_id, entity_instance_id, levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_levelrights (entity_type_id,entity_instance_id,entity_level_id) values ({0},{1},{2})", entity_type_id, entity_instance_id, levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_levelrights (entity_type_id,entity_instance_id,entity_level_id) values ({0},{1},{2})", entity_type_id, entity_instance_id, levelid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into ou_levelrights (entity_type_id,entity_instance_id,entity_level_id) values ({0},{1},{2})", entity_type_id, entity_instance_id, levelid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_level_rights Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_Level_id()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select max(entity_instance_id) + 1 as levelid from entity_defination_master where entity_type_id = 0 order by levelid ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select max(entity_instance_id) + 1 as levelid from entity_defination_master where entity_type_id = 0 order by levelid ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select max(entity_instance_id) + 1 as levelid from entity_defination_master where entity_type_id = 0 order by levelid ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select max(entity_instance_id) + 1 as levelid from entity_defination_master where entity_type_id = 0 order by levelid ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Level_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_OU_Child_Data_Tree()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_parentouid,active from ou_nodelinkage where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_parentouid,active from ou_nodelinkage where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_parentouid,active from ou_nodelinkage where active = 1");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_parentouid,active from ou_nodelinkage where active = 1");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_OU_Child_Data_Tree Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_root_ouid()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select top 1 ou_nodelinkage_ouid from ou_nodelinkage where ou_nodelinkage_levelid=0 and active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid from ou_nodelinkage where ou_nodelinkage_levelid=0 and active = 1 limit 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid from ou_nodelinkage where ou_nodelinkage_levelid=0 and active = 1 limit 1");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_ouid from ou_nodelinkage where ou_nodelinkage_levelid=0 and active = 1 limit 1");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_root_ouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_OU_self_Data(string ou_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_longname,ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid={0} and active = 1", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_longname,ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid={0} and active = 1", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_longname,ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid={0} and active = 1", ou_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_ouid, ou_nodelinkage_nodename,ou_nodelinkage_longname,ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid={0} and active = 1", ou_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_OU_self_Data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_max_ouid()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select max(ou_nodelinkage_ouid)+1 from ou_nodelinkage");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select max(ou_nodelinkage_ouid)+1 from ou_nodelinkage");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select max(ou_nodelinkage_ouid)+1 from ou_nodelinkage");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select max(ou_nodelinkage_ouid)+1 from ou_nodelinkage");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_max_ouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_levelid_of_ouid(string ou_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", ou_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_levelid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", ou_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_levelid_of_ouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_levelid()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id) from entity_defination_master where entity_type_id = 0 and active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id) from entity_defination_master where entity_type_id = 0 and active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id) from entity_defination_master where entity_type_id = 0 and active = 1 ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select max(entity_instance_id) from entity_defination_master where entity_type_id = 0 and active = 1 ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_levelid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_ou_name_count(string ou_name)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and active = 1", ou_name);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and active = 1", ou_name);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and active = 1", ou_name);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and active = 1", ou_name);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_ou_name_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_ouname_oulevel_wise_count(string ou_id, int oulevelid, string ou_name)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage M, ou_allchild D where M.ou_nodelinkage_ouid=D.ou_allchild_child_ou_id and D.ou_allchild_parent_ou_id = {0} and M.ou_nodelinkage_levelid= {1} and M.ou_nodelinkage_nodename='{2}' and M.active=1 ", ou_id, oulevelid, ou_name);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage M, ou_allchild D where M.ou_nodelinkage_ouid=D.ou_allchild_child_ou_id and D.ou_allchild_parent_ou_id = {0} and M.ou_nodelinkage_levelid= {1} and M.ou_nodelinkage_nodename='{2}' and M.active=1 ", ou_id, oulevelid, ou_name);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_nodelinkage M, ou_allchild D where M.ou_nodelinkage_ouid=D.ou_allchild_child_ou_id and D.ou_allchild_parent_ou_id = {0} and M.ou_nodelinkage_levelid= {1} and M.ou_nodelinkage_nodename='{2}' and M.active=1 ", ou_id, oulevelid, ou_name);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_nodelinkage M, ou_allchild D where M.ou_nodelinkage_ouid=D.ou_allchild_child_ou_id and D.ou_allchild_parent_ou_id = {0} and M.ou_nodelinkage_levelid= {1} and M.ou_nodelinkage_nodename='{2}' and M.active=1 ", ou_id, oulevelid, ou_name);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_ouname_oulevel_wise_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        //public string get_ouname_oulevel_wise_count(string ou_name, int oulevelid)
        //{
        //    StringBuilder strQuery = new StringBuilder();
        //    try
        //    {
        //        switch (dbtype)
        //        {
        //            case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
        //                strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and ou_nodelinkage_levelid = {1} and active = 1", ou_name, oulevelid);
        //                break;
        //            case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
        //                strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and ou_nodelinkage_levelid = {1} and active = 1", ou_name, oulevelid);
        //                break;
        //            case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
        //                strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and ou_nodelinkage_levelid = {1} and active = 1", ou_name, oulevelid);
        //                break;
        //            case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
        //                strQuery.AppendFormat(" select count(*) from ou_nodelinkage where ou_nodelinkage_nodename = '{0}' and ou_nodelinkage_levelid = {1} and active = 1", ou_name, oulevelid);
        //                break;
        //        }
        //        objcommon.WriteLog("DBQueryHandler", "get_ouname_oulevel_wise_count Query : " + strQuery.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon.WriteLog("DBQueryHandler", "get_ouname_oulevel_wise_count Exception : " + ex.Message.ToString());
        //    }
        //    return strQuery.ToString();
        //}
        public string insert_new_ou_branch_unit(string ou_id, string parent_ou_id, string levelid, string parent_levelid, string ou_name)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_nodelinkage(ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid,ou_nodelinkage_parentouid,ou_nodelinkage_parentlevelid,startdate,enddate,active) values({0},'{1}',{2},{3},{4},getdate(),null,1)", ou_id, ou_name, levelid, parent_ou_id, parent_levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_nodelinkage(ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid,ou_nodelinkage_parentouid,ou_nodelinkage_parentlevelid,startdate,enddate,active) values({0},'{1}',{2},{3},{4},now(),null,1)", ou_id, ou_name, levelid, parent_ou_id, parent_levelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_nodelinkage(ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid,ou_nodelinkage_parentouid,ou_nodelinkage_parentlevelid,startdate,enddate,active) values({0},'{1}',{2},{3},{4},now(),null,1)", ou_id, ou_name, levelid, parent_ou_id, parent_levelid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into ou_nodelinkage(ou_nodelinkage_ouid,ou_nodelinkage_nodename,ou_nodelinkage_levelid,ou_nodelinkage_parentouid,ou_nodelinkage_parentlevelid,startdate,enddate,active) values({0},'{1}',{2},{3},{4},now(),null,1)", ou_id, ou_name, levelid, parent_ou_id, parent_levelid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_new_ou_branch_unit Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_allchild_insert(string parent_ou_id, string ou_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_allchild(ou_allchild_parent_ou_id, ou_allchild_child_ou_id) values({0},{1})", parent_ou_id, ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_allchild(ou_allchild_parent_ou_id, ou_allchild_child_ou_id) values({0},{1})", parent_ou_id, ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_allchild(ou_allchild_parent_ou_id, ou_allchild_child_ou_id) values({0},{1})", parent_ou_id, ou_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into ou_allchild(ou_allchild_parent_ou_id, ou_allchild_child_ou_id) values({0},{1})", parent_ou_id, ou_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_allchild_insert Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_nodelinkage_ouid_parentouid()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid,ou_nodelinkage_parentouid from ou_nodelinkage where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid,ou_nodelinkage_parentouid from ou_nodelinkage where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_ouid,ou_nodelinkage_parentouid from ou_nodelinkage where active = 1");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_ouid,ou_nodelinkage_parentouid from ou_nodelinkage where active = 1");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_nodelinkage_ouid_parentouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_nodelinkage_parentouid(string parent_ouid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_parentouid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", parent_ouid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_parentouid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", parent_ouid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ou_nodelinkage_parentouid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", parent_ouid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ou_nodelinkage_parentouid from ou_nodelinkage where ou_nodelinkage_ouid = {0} and active = 1", parent_ouid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_nodelinkage_parentouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string location_installation_linkage_child()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select child_locationid from location_installation_linkage where location_linkage_type = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select child_locationid from location_installation_linkage where location_linkage_type = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select child_locationid from location_installation_linkage where location_linkage_type = 1");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select child_locationid from location_installation_linkage where location_linkage_type = 1");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "location_installation_linkage_child Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string native_oulinkage_insert(string locationid, string ou_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into native_oulinkage(locationid,nativeouid) values({0},{1})", locationid, ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into native_oulinkage(locationid,nativeouid) values({0},{1})", locationid, ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into native_oulinkage(locationid,nativeouid) values({0},{1})", locationid, ou_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into native_oulinkage(locationid,nativeouid) values({0},{1})", locationid, ou_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "native_oulinkage_insert Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string execute_inslongname()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("exec inslogname");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("call inslogname");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("call inslogname()");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("call inslogname");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "execute_inslongname Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Update_OUname(string ouid, string ouname)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set  ou_nodelinkage_nodename='{1}' where ou_nodelinkage_ouid= {0}  and active = 1 ", ouid, ouname);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set  ou_nodelinkage_nodename='{1}' where ou_nodelinkage_ouid= {0}  and active = 1 ", ouid, ouname);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set  ou_nodelinkage_nodename='{1}' where ou_nodelinkage_ouid= {0}  and active = 1 ", ouid, ouname);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update ou_nodelinkage set  ou_nodelinkage_nodename='{1}' where ou_nodelinkage_ouid= {0}  and active = 1 ", ouid, ouname);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Update_OUname Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_ou_nodelinkage_ouid(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select distinct ou_nodelinkage_ouid from ou_nodelinkage, ou_allchild where active = 1 and ou_nodelinkage_ouid=ou_allchild_child_ou_id and ou_allchild_parent_ou_id = {0}", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select distinct ou_nodelinkage_ouid from ou_nodelinkage, ou_allchild where active = 1 and ou_nodelinkage_ouid=ou_allchild_child_ou_id and ou_allchild_parent_ou_id = {0}", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select distinct ou_nodelinkage_ouid from ou_nodelinkage, ou_allchild where active = 1 and ou_nodelinkage_ouid=ou_allchild_child_ou_id and ou_allchild_parent_ou_id = {0}", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select distinct ou_nodelinkage_ouid from ou_nodelinkage, ou_allchild where active = 1 and ou_nodelinkage_ouid=ou_allchild_child_ou_id and ou_allchild_parent_ou_id = {0}", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_ou_nodelinkage_ouid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_count(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where ou_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where ou_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where ou_id = {0} and active = 1 ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_entity_relation where ou_id = {0} and active = 1 ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_update_ouidwise(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = getdate(), active = 0 where ou_id in ({0}) and active = 1", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where ou_id in ({0}) and active = 1", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where ou_id in ({0}) and active = 1", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where ou_id in ({0}) and active = 1", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_update_ouidwise Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_count_ou(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = 0 and entity_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = 0 and entity_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = 0 and entity_id = {0} and active = 1 ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = 0 and entity_id = {0} and active = 1 ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_count_ou Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_update_entity_idwise(string entity_type_id, string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_id in ({1}) and active = 1", entity_type_id, id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_id in ({1}) and active = 1", entity_type_id, id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_id in ({1}) and active = 1", entity_type_id, id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_id in ({1}) and active = 1", entity_type_id, id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_update_entity_idwise Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string native_oulinkage_delete(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" delete from native_oulinkage where nativeouid in ({0}) ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" delete from native_oulinkage where nativeouid in ({0}) ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" delete from native_oulinkage where nativeouid in ({0}) ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" delete from native_oulinkage where nativeouid in ({0}) ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "native_oulinkage_delete Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Delete_OU(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set enddate = getdate(), active = 0 where ou_nodelinkage_ouid in ({0}) and active = 1", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set enddate = now(), active = 0 where ou_nodelinkage_ouid in ({0}) and active = 1", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update ou_nodelinkage set enddate = now(), active = 0 where ou_nodelinkage_ouid in ({0}) and active = 1", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update ou_nodelinkage set enddate = now(), active = 0 where ou_nodelinkage_ouid in ({0}) and active = 1", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Delete_OU Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_ouparameterdata()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id,entity_adhoc_type_name from entity_adhoc_type_master where entity_adhoc_type_id not in (1,2,3)");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id,entity_adhoc_type_name from entity_adhoc_type_master where entity_adhoc_type_id not in (1,2,3)");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id,entity_adhoc_type_name from entity_adhoc_type_master where entity_adhoc_type_id not in (1,2,3)");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_adhoc_type_id,entity_adhoc_type_name from entity_adhoc_type_master where entity_adhoc_type_id not in (1,2,3)");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_ouparameterdata Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_adhoc_count(string parameter)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_type_master where entity_adhoc_type_name = '{0}'", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_type_master where entity_adhoc_type_name = '{0}'", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_type_master where entity_adhoc_type_name = '{0}'", parameter);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_adhoc_type_master where entity_adhoc_type_name = '{0}'", parameter);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_adhoc_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ouparamadd(string parameter)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into entity_adhoc_type_master (entity_adhoc_type_name) values('{0}') ", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into entity_adhoc_type_master (entity_adhoc_type_name) values('{0}') ", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into entity_adhoc_type_master (entity_adhoc_type_name) values('{0}') ", parameter);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into entity_adhoc_type_master (entity_adhoc_type_name) values('{0}') ", parameter);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ouparamadd Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_adhoc_info_id()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_adhoc_info_id)+1 from entity_adhoc_info");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_adhoc_info_id)+1 from entity_adhoc_info");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_adhoc_info_id)+1 from entity_adhoc_info");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select max(entity_adhoc_info_id)+1 from entity_adhoc_info");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_adhoc_info_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_startup_menuid(string entity_type_id, string entity_instance_id, string startup_screen, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_startup_menuid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_adhoc_type_id(string parameter)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id from entity_adhoc_type_master where entity_adhoc_type_name = '{0}' ", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id from entity_adhoc_type_master where entity_adhoc_type_name = '{0}' ", parameter);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_type_id from entity_adhoc_type_master where entity_adhoc_type_name = '{0}' ", parameter);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_adhoc_type_id from entity_adhoc_type_master where entity_adhoc_type_name = '{0}' ", parameter);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_adhoc_type_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_otherinfo(string ouid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = 0 and entity_id = '{0}' and eai.entity_adhoc_type_id != 1 and active = 1", ouid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = 0 and entity_id = '{0}' and eai.entity_adhoc_type_id != 1 and active = 1", ouid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = 0 and entity_id = '{0}' and eai.entity_adhoc_type_id != 1 and active = 1", ouid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = 0 and entity_id = '{0}' and eai.entity_adhoc_type_id != 1 and active = 1", ouid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_otherinfo Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_otherinfo_count(string entity_instanceid, string ouid, string entity_adhoc_typeid, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {3} and entity_instance_id = {0} and entity_id = {1} and entity_adhoc_type_id = {2} and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {3} and entity_instance_id = {0} and entity_id = {1} and entity_adhoc_type_id = {2} and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {3} and entity_instance_id = {0} and entity_id = {1} and entity_adhoc_type_id = {2} and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {3} and entity_instance_id = {0} and entity_id = {1} and entity_adhoc_type_id = {2} and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_otherinfo_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string addotherinfo(string entity_instanceid, string entity_adhoc_infoid, string ouid, string entity_adhoc_typeid, string parmvalue, string entity_type_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id,entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values('{5}','{0}','{1}','{2}','{3}','{4}',getdate(),null,1,'{6}') ", entity_instanceid, entity_adhoc_infoid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id,entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values('{5}','{0}','{1}','{2}','{3}','{4}',now(),null,1,'{6}') ", entity_instanceid, entity_adhoc_infoid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id,entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values('{5}','{0}','{1}','{2}','{3}','{4}',now(),null,1,'{6}') ", entity_instanceid, entity_adhoc_infoid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id,entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values('{5}','{0}','{1}','{2}','{3}','{4}',now(),null,1,'{6}') ", entity_instanceid, entity_adhoc_infoid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id, product);
                        //break;
                }
                //objcommon.WriteLog("DBQueryHandler", "addotherinfo Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_editotherinfo(string serialno)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select eai.entity_adhoc_type_id,entity_adhoc_type_name,entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and serial_no = {0} and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select eai.entity_adhoc_type_id,entity_adhoc_type_name,entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and serial_no = {0} and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select eai.entity_adhoc_type_id,entity_adhoc_type_name,entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and serial_no = {0} and active = 1", serialno);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select eai.entity_adhoc_type_id,entity_adhoc_type_name,entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and serial_no = {0} and active = 1", serialno);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_editotherinfo Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string editotherinfo(string entity_instanceid, string ouid, string entity_adhoc_typeid, string parmvalue, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = '{0}',entity_id = '{1}',entity_adhoc_type_id = '{2}',entity_value = '{3}',startdate = getdate(),enddate = null, active = 1 where entity_type_id = {4} and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = '{0}',entity_id = '{1}',entity_adhoc_type_id = '{2}',entity_value = '{3}',startdate = now(),enddate = null, active = 1 where entity_type_id = {4} and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = '{0}',entity_id = '{1}',entity_adhoc_type_id = '{2}',entity_value = '{3}',startdate = now(),enddate = null, active = 1 where entity_type_id = {4} and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = '{0}',entity_id = '{1}',entity_adhoc_type_id = '{2}',entity_value = '{3}',startdate = now(),enddate = null, active = 1 where entity_type_id = {4} and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid, parmvalue, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "editotherinfo Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Delete_info(string serialno)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = getdate(), active = 0 where serial_no = {0} and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where serial_no = {0} and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where serial_no = {0} and active = 1", serialno);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where serial_no = {0} and active = 1", serialno);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Delete_info Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ousystemlinkup(string ou_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        //strQuery.AppendFormat("select device_id, device_name,ip from device_master where device_mode in (1,2,3) ");
                        strQuery.AppendFormat("select dm.device_id, dm.device_name, dm.ip_address, case t.entity_value when isnull(t.entity_value,null) then 1 else 0 end as entity_flag from device_master dm left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = '{0}' and entity_adhoc_type_id = 3 and entity_type_id = 2 and active = 1) t on dm.device_id = t.entity_value where dm.active = 1 ", ou_id);
                        strQuery.AppendFormat("and dm.device_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_type_id = 2 and entity_id <> '{0}' and active = 1) order by dm.device_id ", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select dm.device_id, dm.device_name, dm.ip_address, case t.entity_value when ifnull(t.entity_value,null) then 1 else 0 end as entity_flag from device_master dm left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = '{0}' and entity_adhoc_type_id = 3 and entity_type_id = 2 and active = 1) t on dm.device_id = t.entity_value where dm.active = 1 ", ou_id);
                        strQuery.AppendFormat("and dm.device_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_type_id = 2 and entity_id <> '{0}' and active = 1) order by dm.device_id ", ou_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select dm.device_id, dm.device_name, dm.ip_address, case when t.entity_value is null then 1 else 0 end as entity_flag from device_master dm left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = '{0}' and entity_adhoc_type_id = 3 and entity_type_id = 2 and active = 1) t on dm.device_id = t.entity_value where dm.active = 1 ", ou_id);
                        strQuery.AppendFormat("and dm.device_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_type_id = 2 and entity_id <> '{0}' and active = 1) order by dm.device_id ", ou_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select dm.device_id, dm.device_name, dm.ip_address, case t.entity_value when ifnull(t.entity_value,null) then 1 else 0 end as entity_flag from device_master dm left outer join ");
                    //    strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = '{0}' and entity_adhoc_type_id = 3 and entity_type_id = 2 and active = 1) t on dm.device_id = t.entity_value where dm.active = 1 ", ou_id);
                    //    strQuery.AppendFormat("and dm.device_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_type_id = 2 and entity_id <> '{0}' and active = 1) order by dm.device_id ", ou_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ousystemlinkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_instance_id_device(string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and active = 1", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and active = 1", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and active = 1", entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and active = 1", entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_instance_id_device Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string editousystemlink(string entity_instanceid, string ouid, string entity_adhoc_typeid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = getdate(), active = 0 where entity_type_id = 2 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 2 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 2 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 2 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, ouid, entity_adhoc_typeid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "editousystemlink Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_ouallchild(string ouid, int oulevelid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select ch.ou_allchild_child_ou_id, onl.ou_nodelinkage_nodename from ou_allchild ch left outer join ou_nodelinkage onl on ch.ou_allchild_child_ou_id = onl.ou_nodelinkage_ouid where ch.ou_allchild_parent_ou_id = {0} and onl.ou_nodelinkage_levelid = {1} and onl.active = 1 ", ouid, oulevelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select ch.ou_allchild_child_ou_id, onl.ou_nodelinkage_nodename from ou_allchild ch left outer join ou_nodelinkage onl on ch.ou_allchild_child_ou_id = onl.ou_nodelinkage_ouid where ch.ou_allchild_parent_ou_id = {0} and onl.ou_nodelinkage_levelid = {1} and onl.active = 1 ", ouid, oulevelid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select ch.ou_allchild_child_ou_id, onl.ou_nodelinkage_nodename from ou_allchild ch left outer join ou_nodelinkage onl on ch.ou_allchild_child_ou_id = onl.ou_nodelinkage_ouid where ch.ou_allchild_parent_ou_id = {0} and onl.ou_nodelinkage_levelid = {1} and onl.active = 1 ", ouid, oulevelid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select ch.ou_allchild_child_ou_id, onl.ou_nodelinkage_nodename from ou_allchild ch left outer join ou_nodelinkage onl on ch.ou_allchild_child_ou_id = onl.ou_nodelinkage_ouid where ch.ou_allchild_parent_ou_id = {0} and onl.ou_nodelinkage_levelid = {1} and onl.active = 1 ", ouid, oulevelid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_ouallchild Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        #endregion

        #region settings User Group Management

        public string Get_Groupmgmt()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, case em.entity_name when null then '' else em.entity_name end as entity_name,case em.entity_description when null then '' else em.entity_description end as entity_description,em.entity_instance_id,case edm.entity_instance_name when null then '' else edm.entity_instance_name end as entity_instance_name ");
                        strQuery.AppendFormat("from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 ");
                        strQuery.AppendFormat("group by em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, case em.entity_name when null then '' else em.entity_name end as entity_name,case em.entity_description when null then '' else em.entity_description end as entity_description,em.entity_instance_id,case edm.entity_instance_name when null then '' else edm.entity_instance_name end as entity_instance_name ");
                        strQuery.AppendFormat("from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 ");
                        strQuery.AppendFormat("group by em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, case em.entity_name when null then '' else em.entity_name end as entity_name,case em.entity_description when null then '' else em.entity_description end as entity_description,em.entity_instance_id,case edm.entity_instance_name when null then '' else edm.entity_instance_name end as entity_instance_name ");
                        strQuery.AppendFormat("from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 ");
                        strQuery.AppendFormat("group by em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id, case em.entity_name when null then '' else em.entity_name end as entity_name,case em.entity_description when null then '' else em.entity_description end as entity_description,em.entity_instance_id,case edm.entity_instance_name when null then '' else edm.entity_instance_name end as entity_instance_name ");
                    //    strQuery.AppendFormat("from entity_master em, entity_defination_master edm ");
                    //    strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 ");
                    //    strQuery.AppendFormat("group by em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Groupmgmt Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_user_rights_data(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = getdate(), active = 0 where user_id = {0} and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and active = 1 ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and active = 1 ", user_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_user_rights_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_master_insert(string user_id, string group_id, string menu_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_master (user_id, user_group_id,menu_id,startdate,enddate,active) values({0}, {1}, {2}, getdate(),null,1)", user_id, group_id, menu_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_master (user_id, user_group_id,menu_id,startdate,enddate,active) values({0}, {1}, {2}, now(),null,1)", user_id, group_id, menu_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_master (user_id, user_group_id,menu_id,startdate,enddate,active) values({0}, {1}, {2}, now(),null,1)", user_id, group_id, menu_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into access_rights_master (user_id, user_group_id,menu_id,startdate,enddate,active) values({0}, {1}, {2}, now(),null,1)", user_id, group_id, menu_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_master_insert Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_type(string entity_type_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id, entity_instance_name from entity_defination_master where entity_type_id = {0} and product_type = {1} and active = 1 order by entity_instance_id", entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id, entity_instance_name from entity_defination_master where entity_type_id = {0} and product_type = {1} and active = 1 order by entity_instance_id", entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id, entity_instance_name from entity_defination_master where entity_type_id = {0} and product_type = {1} and active = 1 order by entity_instance_id", entity_type_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_instance_id, entity_instance_name from entity_defination_master where entity_type_id = {0} and product_type = {1} and active = 1 order by entity_instance_id", entity_type_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_type Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_group_count(string groupname, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and active = 1", groupname, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and active = 1", groupname, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and active = 1", groupname, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and active = 1", groupname, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_group_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_user_count(string groupname, string entity_type_id, string instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and entity_instance_id = {2} and active = 1", groupname, entity_type_id, instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and entity_instance_id = {2} and active = 1", groupname, entity_type_id, instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and entity_instance_id = {2} and active = 1", groupname, entity_type_id, instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_master where entity_type_id = {1} and entity_name = '{0}' and entity_instance_id = {2} and active = 1", groupname, entity_type_id, instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_user_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_master_id()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_id)+1 from entity_master");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_id)+1 from entity_master");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_id)+1 from entity_master");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select max(entity_id)+1 from entity_master");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_master_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string group_rights_data(string product_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where product_type = {0} and active = 1", product_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where product_type = {0} and active = 1", product_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where product_type = {0} and active = 1", product_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where product_type = {0} and active = 1", product_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "group_rights_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string group_rights_data()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where active = 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where active = 1");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_id,menu_name,menu_parent_id,active from menu_master where active = 1");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "group_rights_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string addgroup(string grouptype, string entity_id, string groupname, string groupdescription, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_master (entity_type_id, entity_instance_id,entity_id,entity_name,entity_description,startdate,enddate,active) values({4},'{0}','{1}','{2}','{3}',getdate(),null,1) ", grouptype, entity_id, groupname, groupdescription, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_master (entity_type_id, entity_instance_id,entity_id,entity_name,entity_description,startdate,enddate,active) values({4},'{0}','{1}','{2}','{3}',now(),null,1) ", grouptype, entity_id, groupname, groupdescription, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_master (entity_type_id, entity_instance_id,entity_id,entity_name,entity_description,startdate,enddate,active) values({4},'{0}','{1}','{2}','{3}',now(),null,1) ", grouptype, entity_id, groupname, groupdescription, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into entity_master (entity_type_id, entity_instance_id,entity_id,entity_name,entity_description,startdate,enddate,active) values({4},'{0}','{1}','{2}','{3}',now(),null,1) ", grouptype, entity_id, groupname, groupdescription, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "addgroup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_group_data(string groupid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 and entity_id = {0} ", groupid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 and entity_id = {0} ", groupid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name from entity_master em, entity_defination_master edm ");
                        strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 and entity_id = {0} ", groupid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id, em.entity_name,em.entity_description,em.entity_instance_id,edm.entity_instance_name from entity_master em, entity_defination_master edm ");
                    //    strQuery.AppendFormat("where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 4 and em.active = 1 and entity_id = {0} ", groupid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_group_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string editgroup(string grouptype, string groupdescription, string groupid, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = {0}, entity_description = '{2}' where entity_type_id = {3} and entity_id = {1} and active = 1 ", grouptype, groupid, groupdescription, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = {0}, entity_description = '{2}' where entity_type_id = {3} and entity_id = {1} and active = 1 ", grouptype, groupid, groupdescription, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = {0}, entity_description = '{2}' where entity_type_id = {3} and entity_id = {1} and active = 1 ", grouptype, groupid, groupdescription, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_master set entity_instance_id = {0}, entity_description = '{2}' where entity_type_id = {3} and entity_id = {1} and active = 1 ", grouptype, groupid, groupdescription, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "editgroup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_master_update_groupid(string group_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = getdate(), active = 0 where user_group_id = {0} and active = 1 ", group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_group_id = {0} and active = 1 ", group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_group_id = {0} and active = 1 ", group_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_group_id = {0} and active = 1 ", group_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_master_update_groupid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_master_count_group(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_group_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_group_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_group_id = {0} and active = 1 ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from access_rights_master where user_group_id = {0} and active = 1 ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_master_count_group Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string deletegroupdata(string groupid, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                strQuery = new StringBuilder();
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set enddate = getdate(), active = 0 where entity_type_id = {1} and entity_id = {0} and active = 1", groupid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set enddate = now(), active = 0 where entity_type_id = {1} and entity_id = {0} and active = 1", groupid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set enddate = now(), active = 0 where entity_type_id = {1} and entity_id = {0} and active = 1", groupid, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_master set enddate = now(), active = 0 where entity_type_id = {1} and entity_id = {0} and active = 1", groupid, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "deletegroupdata Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        #endregion

        #region User Type

        public string Get_usertype_datatable(string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select edm.serial_no,edm.entity_instance_id,edm.entity_instance_name, art.entity_type_id, art.menu_id, mm.menu_name, ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type where edm.entity_type_id = {0} and art.startup_screen = 1 and edm.active = 1 order by ptm.product_type_name, edm.entity_instance_name ", entity_type_id);
                        //strQuery.AppendFormat("select serial_no,entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select edm.serial_no,edm.entity_instance_id,edm.entity_instance_name, art.entity_type_id, art.menu_id, mm.menu_name, ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type where edm.entity_type_id = {0} and art.startup_screen = 1 and edm.active = 1 order by ptm.product_type_name, edm.entity_instance_name ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select edm.serial_no,edm.entity_instance_id,edm.entity_instance_name, art.entity_type_id, art.menu_id, mm.menu_name, ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type where edm.entity_type_id = {0} and art.startup_screen = 1 and edm.active = 1 order by ptm.product_type_name, edm.entity_instance_name ", entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select edm.serial_no,edm.entity_instance_id,edm.entity_instance_name, art.entity_type_id, art.menu_id, mm.menu_name, ptm.product_type_id, ptm.product_type_name ");
                    //    strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                    //    strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type where edm.entity_type_id = {0} and art.startup_screen = 1 and edm.active = 1 order by ptm.product_type_name, edm.entity_instance_name ", entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_usertype_datatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_product_type()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id, product_type_name from product_type_master ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id, product_type_name from product_type_master ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id, product_type_name from product_type_master ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select product_type_id, product_type_name from product_type_master ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_product_type Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_product_type_id(string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id from product_type_master where product_type_name = '{0}' ", product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id from product_type_master where product_type_name = '{0}' ", product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select product_type_id from product_type_master where product_type_name = '{0}' ", product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select product_type_id from product_type_master where product_type_name = '{0}' ", product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_product_type_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_instance_id_usertype(string entity_type_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id)+1 from entity_defination_master where entity_type_id = {0} and product_type = {1}", entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id)+1 from entity_defination_master where entity_type_id = {0} and product_type = {1}", entity_type_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select max(entity_instance_id)+1 from entity_defination_master where entity_type_id = {0} and product_type = {1}", entity_type_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select max(entity_instance_id)+1 from entity_defination_master where entity_type_id = {0} and product_type = {1} ", entity_type_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_instance_id_usertype Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_instance_id_name_usertype(string entity_type_id, string user_type, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_instance_id from entity_defination_master where entity_type_id = {0} and entity_instance_name = '{1}' and product_type = {2} and active = 1 ", entity_type_id, user_type, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_instance_id_name_usertype Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string save_usertype_data_count(string user_type, string product, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and entity_type_id = {2} and active = 1  ", user_type, product, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and entity_type_id = {2} and active = 1  ", user_type, product, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and entity_type_id = {2} and active = 1  ", user_type, product, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and entity_type_id = {2} and active = 1  ", user_type, product, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "save_usertype_data_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_cnt(string entity_type_id, string entity_instance_id, string startup_screen, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(menu_id) from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(menu_id) from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(menu_id) from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(menu_id) from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_cnt Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_delete(string entity_type_id, string entity_instance_id, string startup_screen, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and startup_screen = {2} and product_type = {3} and active = 1 ", entity_type_id, entity_instance_id, startup_screen, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_delete Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_insert(string entity_type_id, string instance_id, string menu_id, string startup_screen, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_template (entity_type_id,entity_instance_id,menu_id,startup_screen,startdate,enddate,active,product_type) values({0}, {1}, {2}, {3}, getdate(),null,1, {4})", entity_type_id, instance_id, menu_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_template (entity_type_id,entity_instance_id,menu_id,startup_screen,startdate,enddate,active,product_type) values({0}, {1}, {2}, {3}, now(),null,1, {4})", entity_type_id, instance_id, menu_id, startup_screen, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into access_rights_template (entity_type_id,entity_instance_id,menu_id,startup_screen,startdate,enddate,active,product_type) values({0}, {1}, {2}, {3}, now(),null,1, {4})", entity_type_id, instance_id, menu_id, startup_screen, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into access_rights_template (entity_type_id,entity_instance_id,menu_id,startup_screen,startdate,enddate,active,product_type) values({0}, {1}, {2}, {3}, now(),null,1, {4})", entity_type_id, instance_id, menu_id, startup_screen, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_insert Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string update_entity_defination_master(string user_type, string serialno, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name = '{0}' where entity_type_id = {2} and serial_no = {1} and active = 1 ", user_type, serialno, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name = '{0}' where entity_type_id = {2} and serial_no = {1} and active = 1 ", user_type, serialno, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set entity_instance_name = '{0}' where entity_type_id = {2} and serial_no = {1} and active = 1 ", user_type, serialno, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_defination_master set entity_instance_name = '{0}' where entity_type_id = {2} and serial_no = {1} and active = 1 ", user_type, serialno, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "update_entity_defination_master Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_update(string startup, string instance_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update access_rights_template set menu_id = {0} where entity_type_id = 1 and entity_instance_id = {1} and product_type = {2} and active = 1", startup, instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update access_rights_template set menu_id = {0} where entity_type_id = 1 and entity_instance_id = {1} and product_type = {2} and active = 1", startup, instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update access_rights_template set menu_id = {0} where entity_type_id = 1 and entity_instance_id = {1} and product_type = {2} and active = 1", startup, instance_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update access_rights_template set menu_id = {0} where entity_type_id = 1 and entity_instance_id = {1} and product_type = {2} and active = 1", startup, instance_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_update Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_usertype_id(string entity_type_id, string user_type)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select entity_instance_name from entity_defination_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, user_type);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select entity_instance_name from entity_defination_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, user_type);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select entity_instance_name from entity_defination_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, user_type);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select entity_instance_name from entity_defination_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, user_type);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_usertype_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_edit_usertypedata(string instance_name, string entity_type_id, string product_type)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_instance_id,edm.entity_instance_name, art.menu_id, mm.menu_name,ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type ");
                        strQuery.AppendFormat("where edm.entity_type_id = {1} and edm.entity_instance_name = '{0}' and art.startup_screen = 1 and ptm.product_type_id = {2} and edm.active = 1 ", instance_name, entity_type_id, product_type);
                        //strQuery.AppendFormat("select entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {1} and serial_no = {0} and active = 1 ", serialno, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_instance_id,edm.entity_instance_name, art.menu_id, mm.menu_name,ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type ");
                        strQuery.AppendFormat("where edm.entity_type_id = {1} and edm.entity_instance_name = '{0}' and art.startup_screen = 1 and ptm.product_type_id = {2} and edm.active = 1 ", instance_name, entity_type_id, product_type);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select edm.entity_instance_id,edm.entity_instance_name, art.menu_id, mm.menu_name,ptm.product_type_id, ptm.product_type_name ");
                        strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                        strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type ");
                        strQuery.AppendFormat("where edm.entity_type_id = {1} and edm.entity_instance_name = '{0}' and art.startup_screen = 1 and ptm.product_type_id = {2} and edm.active = 1 ", instance_name, entity_type_id, product_type);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select edm.entity_instance_id,edm.entity_instance_name, art.menu_id, mm.menu_name,ptm.product_type_id, ptm.product_type_name ");
                    //    strQuery.AppendFormat("from access_rights_template art inner join entity_defination_master edm on edm.entity_instance_id = art.entity_instance_id and edm.active = art.active ");
                    //    strQuery.AppendFormat("left outer join menu_master mm on art.menu_id = mm.menu_id left outer join product_type_master ptm on ptm.product_type_id = mm.product_type ");
                    //    strQuery.AppendFormat("where edm.entity_type_id = {1} and edm.entity_instance_name = '{0}' and art.startup_screen = 1 and ptm.product_type_id = {2} and edm.active = 1 ", instance_name, entity_type_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_edit_usertypedata Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_ouLevel_RightsCount(string entity_type_id, string entity_instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type_id, entity_instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type_id, entity_instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_ouLevel_RightsCount Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_entity_masterCount(string entity_type_id, string entity_instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_entity_masterCount Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_master_userids(string entity_type_id, string entity_instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select entity_id from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select entity_id from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select entity_id from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select entity_id from entity_master where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_master_userids Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_adhoc_info_count(string entity_type_id, string entity_instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from entity_adhoc_info where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_adhoc_info_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_ou_entity_relation_count(string entity_type_id, string entity_instance_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type_id, entity_instance_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_ou_entity_relation_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_defination_master_entity_instancename(string instance_name, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and active = 1 ", instance_name, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and active = 1 ", instance_name, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and active = 1 ", instance_name, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where entity_instance_name = '{0}' and product_type = {1} and active = 1 ", instance_name, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_defination_master_entity_typeid_instanceid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_defination_master_entity_typeid_instanceid(string serialno)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where serial_no = {0} and active = 1 ", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where serial_no = {0} and active = 1 ", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where serial_no = {0} and active = 1 ", serialno);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_type_id, entity_instance_id from entity_defination_master where serial_no = {0} and active = 1 ", serialno);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_defination_master_entity_typeid_instanceid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_levelrights_delete_entity_typeid_instanceid(string entity_type, string entity_instance)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("delete from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type, entity_instance);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("delete from ou_levelrights where entity_type_id = {0} and entity_instance_id = {1} ", entity_type, entity_instance);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_levelrights_delete_entity_typeid_instanceid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_master_update_delete(string entity_type, string entity_instance)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update entity_master set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update entity_master set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update entity_master set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update entity_master set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                    //    break;
                }
                ///*objcommon.WriteLog("DBQueryHandler",*/ "entity_master_update_delete Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_update_delete(string entity_type, string entity_instance)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_update_delete Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_update_delete(string entity_type, string entity_instance)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and active = 1 ", entity_type, entity_instance);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_update_delete Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_deleteall(string entity_type_id, string entity_instance_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_template set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_deleteall Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_usertype_entity_defination_master(string instance_name, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = getdate(), active = 0 where entity_instance_name = '{0}' and product_type = {1} and active = 1", instance_name, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = getdate(), active = 0 where entity_instance_name = '{0}' and product_type = {1} and active = 1", instance_name, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = getdate(), active = 0 where entity_instance_name = '{0}' and product_type = {1} and active = 1", instance_name, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_defination_master set enddate = getdate(), active = 0 where entity_instance_name = '{0}' and product_type = {1} and active = 1", instance_name, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_usertype_entity_defination_master Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_usertype_entity_defination_master(string serialno)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = getdate(), active = 0 where serial_no = '{0}' and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = now(), active = 0 where serial_no = '{0}' and active = 1", serialno);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_defination_master set enddate = now(), active = 0 where serial_no = '{0}' and active = 1", serialno);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_defination_master set enddate = now(), active = 0 where serial_no = '{0}' and active = 1", serialno);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_usertype_entity_defination_master Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region Setting User Management 

        public string Get_startup_page(string product_type)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name from menu_master where product_type = {0} and active = 1 order by menu_id", product_type);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name from menu_master where product_type = {0} and active = 1 order by menu_id", product_type);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select menu_id,menu_name from menu_master where product_type = {0} and active = 1 order by menu_id", product_type);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select menu_id,menu_name from menu_master where product_type = {0} and active = 1 order by menu_id", product_type);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_startup_page Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_startup(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id  = 2 and entity_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id  = 2 and entity_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id  = 2 and entity_type_id = 1 and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id  = 2 and entity_type_id = 1 and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_startup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_Usermgmt()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,case em.entity_name when null then '' else em.entity_name end as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name,usertype, username ");
                        //strQuery.AppendFormat("select em.entity_id as userid,case em.entity_name when null then '' else em.entity_name end as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 1 and em.active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,case em.entity_name when null then '' else em.entity_name end as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name, usertype, username ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,case em.entity_name when null then '' else em.entity_name end as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name, usertype, username ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id as userid,case em.entity_name when null then '' else em.entity_name end as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                    //    strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name, usertype, username ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_Usermgmt Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_user_password(string usertype, string adhoc_infoid, string entity_id, string encryptpasswoed, string product)
        {
            //password save
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',1,'{3}',getdate(),null,1,{4}) ", usertype, adhoc_infoid, entity_id, encryptpasswoed, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',1,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid, entity_id, encryptpasswoed, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',1,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid, entity_id, encryptpasswoed, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',1,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid, entity_id, encryptpasswoed, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_user_password Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_user_startupscreen(string usertype, string adhoc_infoid_startscreen, string entity_id, string startup, string product)
        {
            //StartUp Screen save
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',2,'{3}',getdate(),null,1,{4}) ", usertype, adhoc_infoid_startscreen, entity_id, startup, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',2,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid_startscreen, entity_id, startup, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',2,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid_startscreen, entity_id, startup, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into entity_adhoc_info (entity_type_id, entity_instance_id,entity_adhoc_info_id,entity_id,entity_adhoc_type_id,entity_value,startdate,enddate,active,product_type) values(1,'{0}','{1}','{2}',2,'{3}',now(),null,1,{4}) ", usertype, adhoc_infoid_startscreen, entity_id, startup, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_user_startupscreen Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_template_menuid(string entity_type_id, string entity_instance_id, string product)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select distinct menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select distinct menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select distinct menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select distinct menu_id from access_rights_template where entity_type_id = {0} and entity_instance_id = {1} and product_type = {2} and active = 1 ", entity_type_id, entity_instance_id, product);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_template_menuid Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_password(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select entity_value from entity_adhoc_info where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select entity_value from entity_adhoc_info where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select entity_value from entity_adhoc_info where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select entity_value from entity_adhoc_info where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_password Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string set_reset_password(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = getdate(), active = 0 where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 1 and entity_id = {0} and entity_adhoc_type_id = 1 and active = 1 ", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "set_reset_password Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string set_reset_check_old_password(string oldpassword, string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_info where entity_type_id = 1 and entity_adhoc_type_id = 1 and active = 1 and entity_id = {1} and entity_value = '{0}' ", oldpassword, userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_info where entity_type_id = 1 and entity_adhoc_type_id = 1 and active = 1 and entity_id = {1} and entity_value = '{0}' ", oldpassword, userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from entity_adhoc_info where entity_type_id = 1 and entity_adhoc_type_id = 1 and active = 1 and entity_id = {1} and entity_value = '{0}' ", oldpassword, userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(*) from entity_adhoc_info where entity_type_id = 1 and entity_adhoc_type_id = 1 and active = 1 and entity_id = {1} and entity_value = '{0}' ", oldpassword, userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "set_reset_check_old_password Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_update_delete_user(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = getdate(), active = 0 where entity_id = {0} and entity_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where entity_id = {0} and entity_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where entity_id = {0} and entity_type_id = 1 and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update ou_entity_relation set enddate = now(), active = 0 where entity_id = {0} and entity_type_id = 1 and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_update_delete_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_count_user(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_id = {0} and active = 1 ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from ou_entity_relation where entity_id = {0} and active = 1 ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_count_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_master_count_user(string id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_master_count_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_otherinfo_user(string userid, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = {1} and entity_id = '{0}' and eai.entity_adhoc_type_id not in (1,2,3) and active = 1", userid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = {1} and entity_id = '{0}' and eai.entity_adhoc_type_id not in (1,2,3) and active = 1", userid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = {1} and entity_id = '{0}' and eai.entity_adhoc_type_id not in (1,2,3) and active = 1", userid, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select serial_no,eai.entity_adhoc_type_id,case entity_adhoc_type_name when null then '' else entity_adhoc_type_name end as entity_adhoc_type_name,case entity_value when null then '' else entity_value end as entity_value from entity_adhoc_info eai, entity_adhoc_type_master eat where eai.entity_adhoc_type_id = eat.entity_adhoc_type_id and entity_type_id = {1} and entity_id = '{0}' and eai.entity_adhoc_type_id not in (1,2,3) and active = 1", userid, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_otherinfo_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_instance_id_user(string userid, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_master where entity_type_id = {1} and entity_id = '{0}' and active = 1", userid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_master where entity_type_id = {1} and entity_id = '{0}' and active = 1", userid, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id from entity_master where entity_type_id = {1} and entity_id = '{0}' and active = 1", userid, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_instance_id from entity_master where entity_type_id = {1} and entity_id = '{0}' and active = 1", userid, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_instance_id_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_user_data(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.entity_instance_id as usertypeid, edm.entity_instance_name as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 and em.entity_id = {0} ", userid);
                        //strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.entity_instance_id as usertypeid, edm.entity_instance_name as usertype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 1 and em.active = 1 and em.entity_id = {0} ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.entity_instance_id as usertypeid, edm.entity_instance_name as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 and em.entity_id = {0} ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.entity_instance_id as usertypeid, edm.entity_instance_name as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 and em.entity_id = {0} ", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.entity_instance_id as usertypeid, edm.entity_instance_name as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                    //    strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.entity_type_id = 1 and em.active = 1 and em.entity_id = {0} ", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_user_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_edit_entity_adhoc_info_id(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 1 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 1 and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 1 and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_edit_entity_adhoc_info_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_edit_entity_adhoc_info_id_startscreen(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 2 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 2 and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 2 and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_adhoc_info_id from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 2 and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_edit_entity_adhoc_info_id_startscreen Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string access_rights_master_count(string entity_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", entity_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" select count(*) from access_rights_master where user_id = {0} and active = 1 ", entity_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "access_rights_master_count Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string edituser(string usertype, string entity_id, string username)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = '{0}' where entity_id = {1} and entity_name = '{2}' and active = 1 ", usertype, entity_id, username);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = '{0}' where entity_id = {1} and entity_name = '{2}' and active = 1 ", usertype, entity_id, username);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_master set entity_instance_id = '{0}' where entity_id = {1} and entity_name = '{2}' and active = 1 ", usertype, entity_id, username);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_master set entity_instance_id = '{0}' where entity_id = {1} and entity_name = '{2}' and active = 1 ", usertype, entity_id, username);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "edituser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_user_password_update(string usertype, string adhoc_infoid, string entity_id, string encryptpassword)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_adhoc_info_id = {1},entity_value = '{3}' where entity_id = {2} and entity_adhoc_type_id = 1 and active = 1", usertype, adhoc_infoid, entity_id, encryptpassword);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_adhoc_info_id = {1},entity_value = '{3}' where entity_id = {2} and entity_adhoc_type_id = 1 and active = 1", usertype, adhoc_infoid, entity_id, encryptpassword);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_adhoc_info_id = {1},entity_value = '{3}' where entity_id = {2} and entity_adhoc_type_id = 1 and active = 1", usertype, adhoc_infoid, entity_id, encryptpassword);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_adhoc_info_id = {1},entity_value = '{3}' where entity_id = {2} and entity_adhoc_type_id = 1 and active = 1", usertype, adhoc_infoid, entity_id, encryptpassword);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_user_password_update Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_adhoc_info_user_startupscreen_update(string usertype, string entity_id, string startup)
        {
            //StartUp Screen save
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_value = '{2}' where entity_id = {1} and entity_adhoc_type_id = 2 and active = 1", usertype, entity_id, startup);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_value = '{2}' where entity_id = {1} and entity_adhoc_type_id = 2 and active = 1", usertype, entity_id, startup);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_value = '{2}' where entity_id = {1} and entity_adhoc_type_id = 2 and active = 1", usertype, entity_id, startup);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set entity_instance_id = {0}, entity_value = '{2}' where entity_id = {1} and entity_adhoc_type_id = 2 and active = 1", usertype, entity_id, startup);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_adhoc_info_user_startupscreen_update Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_update_user(string entity_id, string usertype)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set entity_instance_id = {0} where entity_id = {1} and entity_type_id = 1 and active = 1", usertype, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set entity_instance_id = {0} where entity_id = {1} and entity_type_id = 1 and active = 1", usertype, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update ou_entity_relation set entity_instance_id = {0} where entity_id = {1} and entity_type_id = 1 and active = 1", usertype, entity_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update ou_entity_relation set entity_instance_id = {0} where entity_id = {1} and entity_type_id = 1 and active = 1", usertype, entity_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_update_user Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region Link User

        public string Get_linkuser_datatable()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id ");
                        strQuery.AppendFormat("where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name,edm.entity_instance_name,em.entity_name ");
                        //strQuery.AppendFormat("select entity_id,entity_name from entity_master where active = 1 and entity_type_id = 1 and entity_id != 1");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id ");
                        strQuery.AppendFormat("where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name,edm.entity_instance_name,em.entity_name ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                        strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id ");
                        strQuery.AppendFormat("where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name,edm.entity_instance_name,em.entity_name ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, em.active from entity_master em left outer join ");
                    //    strQuery.AppendFormat("entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id ");
                    //    strQuery.AppendFormat("where em.entity_type_id = 1 and em.active = 1 order by ptm.product_type_name,edm.entity_instance_name,em.entity_name ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_linkuser_datatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_linkgroup(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select distinct em.entity_id,em.entity_name,case t.user_group_id when isnull(t.user_group_id,null) then 1 else 0 end as group_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select user_group_id from access_rights_master where user_id = {0} and active = 1) t on em.entity_id = t.user_group_id where em.active = 1 and em.entity_type_id = 4 order by em.entity_id ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select distinct em.entity_id,em.entity_name,case t.user_group_id when ifnull(t.user_group_id,null) then 1 else 0 end as group_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select user_group_id from access_rights_master where user_id = {0} and active = 1) t on em.entity_id = t.user_group_id where em.active = 1 and em.entity_type_id = 4 order by em.entity_id ", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select distinct em.entity_id,em.entity_name,case when t.user_group_id is null then 0 else 1 end as group_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select user_group_id from access_rights_master where user_id = {0} and active = 1) t on em.entity_id = t.user_group_id where em.active = 1 and em.entity_type_id = 4 order by em.entity_id ", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select distinct em.entity_id,em.entity_name,case t.user_group_id when ifnull(t.user_group_id,null) then 1 else 0 end as group_flag from entity_master em left outer join ");
                    //    strQuery.AppendFormat("(select user_group_id from access_rights_master where user_id = {0} and active = 1) t on em.entity_id = t.user_group_id where em.active = 1 and em.entity_type_id = 4 order by em.entity_id ", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_linkgroup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_user_name(string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_id = {0} and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_id = {0} and active = 1", userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_id = {0} and active = 1", userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_name from entity_master where entity_id = {0} and active = 1", userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_user_name Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_menuid_linkuser(string groupid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_group_id = {0} and user_id = 0 and active = 1", groupid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_group_id = {0} and user_id = 0 and active = 1", groupid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_group_id = {0} and user_id = 0 and active = 1", groupid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select distinct menu_id from access_rights_master where user_group_id = {0} and user_id = 0 and active = 1", groupid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_menuid_linkuser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string check_user_group_id_linkuser(string user_id, string group_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "check_user_group_id_linkuser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string check_user_id_linkuser(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(distinct menu_id) from access_rights_master where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "check_user_id_linkuser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_userid_linkuser(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = getdate(), active = 0 where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id != 0 and active = 1 ", user_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_userid_linkuser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_user_rights_data_linkuser(string user_id, string group_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = getdate(), active = 0 where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update access_rights_master set enddate = now(), active = 0 where user_id = {0} and user_group_id = {1} and active = 1 ", user_id, group_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_user_rights_data_linkuser Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_ou_data()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname from ou_nodelinkage onl where onl.active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname from ou_nodelinkage onl where onl.active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname from ou_nodelinkage onl where onl.active = 1 ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname from ou_nodelinkage onl where onl.active = 1 ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_ou_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_ou_entity_relation(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname,t.ou_access, case t.ou_id when isnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id,ou_access from ou_entity_relation where entity_id = {0} and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname,t.ou_access, case t.ou_id when ifnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id,ou_access from ou_entity_relation where entity_id = {0} and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname,t.ou_access, case when t.ou_id is null then 0 else 1 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id,ou_access from ou_entity_relation where entity_id = {0} and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select onl.ou_nodelinkage_ouid,onl.ou_nodelinkage_longname,t.ou_access, case t.ou_id when ifnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                    //    strQuery.AppendFormat("(select ou_id,ou_access from ou_entity_relation where entity_id = {0} and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 ", user_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_ou_entity_relation Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entity_master_userlinkup_data(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id,entity_id,entity_name from entity_master where entity_type_id = 1 and entity_id in ({0}) and active = 1", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id,entity_id,entity_name from entity_master where entity_type_id = 1 and entity_id in ({0}) and active = 1", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_type_id,entity_instance_id,entity_id,entity_name from entity_master where entity_type_id = 1 and entity_id in ({0}) and active = 1", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_type_id,entity_instance_id,entity_id,entity_name from entity_master where entity_type_id = 1 and entity_id in ({0}) and active = 1", user_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entity_master_userlinkup_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string check_user_exist_ou_linkup(string entity_type_id, string entity_instance_id, string entity_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                        //    case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                        //        strQuery.AppendFormat("select count(*) from ou_entity_relation where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        //        break;
                        //}
                        //objcommon.WriteLog("DBQueryHandler", "check_user_exist_ou_linkup Query : " + strQuery.ToString());
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string ou_entity_relation_update_delete_oulinkup(string entity_type_id, string entity_instance_id, string entity_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = getdate(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where entity_type_id = {0} and entity_instance_id = {1} and entity_id = {2} and active = 1", entity_type_id, entity_instance_id, entity_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "ou_entity_relation_update_delete_oulinkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string insert_update_user_ou_linkup(string entity_type_id, string entity_instance_id, string ou_id, string entity_id, string child)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values ({0},{1},'{2}',{3},{4},getdate(),null,1)", entity_type_id, entity_instance_id, ou_id, entity_id, child);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values ({0},{1},'{2}',{3},{4},now(),null,1)", entity_type_id, entity_instance_id, ou_id, entity_id, child);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values ({0},{1},'{2}',{3},{4},now(),null,1)", entity_type_id, entity_instance_id, ou_id, entity_id, child);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values ({0},{1},'{2}',{3},{4},now(),null,1)", entity_type_id, entity_instance_id, ou_id, entity_id, child);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_update_user_ou_linkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Delete_link_ou(string ou_nodelinkage_ouid, string userid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = getdate(), active = 0 where ou_id = {0} and entity_id = {1} and active = 1", ou_nodelinkage_ouid, userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where ou_id = {0} and entity_id = {1} and active = 1", ou_nodelinkage_ouid, userid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where ou_id = {0} and entity_id = {1} and active = 1", ou_nodelinkage_ouid, userid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("update ou_entity_relation set enddate = now(), active = 0 where ou_id = {0} and entity_id = {1} and active = 1", ou_nodelinkage_ouid, userid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Delete_link_ou Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region Manage User Rights

        public string get_username(string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_id,entity_name from entity_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_id,entity_name from entity_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_id,entity_name from entity_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_id,entity_name from entity_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_username Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);

            }
            return strQuery.ToString();
        }
        public string showuserdatatable(string str_search)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select userid,case username when null then '' else username end as username,groupid,isnull(groupname, '') as groupname from ( ");
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.active from entity_master em where em.entity_type_id = 1 and em.active = 1) t left outer join ");
                        strQuery.AppendFormat("(select distinct arm.user_group_id as groupid, em.entity_name as groupname,arm.user_id, em.active from entity_master em, access_rights_master arm where em.entity_id = arm.user_group_id and em.entity_type_id = 4 and arm.active = 1) s on t.userid=s.user_id and t.active = s.active where t.active = 1 {0}", str_search);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select userid,case username when null then '' else username end as username,groupid,ifnull(groupname, '') as groupname from ( ");
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.active from entity_master em where em.entity_type_id = 1 and em.active = 1) t left outer join ");
                        strQuery.AppendFormat("(select distinct arm.user_group_id as groupid, em.entity_name as groupname,arm.user_id, em.active from entity_master em, access_rights_master arm where em.entity_id = arm.user_group_id and em.entity_type_id = 4 and arm.active = 1) s on t.userid=s.user_id and t.active = s.active where t.active = 1 {0}", str_search);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select userid,case username when null then '' else username end as username,groupid,isnull(groupname, '') as groupname from ( ");
                        strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.active from entity_master em where em.entity_type_id = 1 and em.active = 1) t left outer join ");
                        strQuery.AppendFormat("(select distinct arm.user_group_id as groupid, em.entity_name as groupname,arm.user_id, em.active from entity_master em, access_rights_master arm where em.entity_id = arm.user_group_id and em.entity_type_id = 4 and arm.active = 1) s on t.userid=s.user_id and t.active = s.active where t.active = 1 {0}", str_search);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select userid,case username when null then '' else username end as username,groupid,isnull(groupname, '') as groupname from ( ");
                    //    strQuery.AppendFormat("select em.entity_id as userid,em.entity_name as username, em.active from entity_master em where em.entity_type_id = 1 and em.active = 1) t left outer join ");
                    //    strQuery.AppendFormat("(select distinct arm.user_group_id as groupid, em.entity_name as groupname,arm.user_id, em.active from entity_master em, access_rights_master arm where em.entity_id = arm.user_group_id and em.entity_type_id = 4 and arm.active = 1) s on t.userid=s.user_id and t.active = s.active where t.active = 1 {0}", str_search);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "showuserdatatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string check_user_id(string user_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct user_id) from access_rights_master where user_id = {0} and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct user_id) from access_rights_master where user_id = {0} and active = 1 ", user_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select count(distinct user_id) from access_rights_master where user_id = {0} and active = 1 ", user_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select count(distinct user_id) from access_rights_master where user_id = {0} and active = 1 ", user_id);
                    //    break;
                }
                //objcommon.WxriteLog("DBQueryHandler", "check_user_id Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region Connect Parent
        public string get_parent_data()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select parent_ouid,parent_locationid,parentip from location_installation_linkage where location_linkage_type = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select parent_ouid,parent_locationid,parentip from location_installation_linkage where location_linkage_type = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select parent_ouid,parent_locationid,parentip from location_installation_linkage where location_linkage_type = 1 ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select parent_ouid,parent_locationid,parentip from location_installation_linkage where location_linkage_type = 1 ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_parent_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string update_location_installation_linkage(string ou_id, string location_id, string ip)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update location_installation_linkage set parent_ouid = '{0}',parent_locationid = '{1}',parentip = '{2}' where location_linkage_type = 1", ou_id, location_id, ip);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update location_installation_linkage set parent_ouid = '{0}',parent_locationid = '{1}',parentip = '{2}' where location_linkage_type = 1", ou_id, location_id, ip);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update location_installation_linkage set parent_ouid = '{0}',parent_locationid = '{1}',parentip = '{2}' where location_linkage_type = 1", ou_id, location_id, ip);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update location_installation_linkage set parent_ouid = '{0}',parent_locationid = '{1}',parentip = '{2}' where location_linkage_type = 1", ou_id, location_id, ip);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "update_location_installation_linkage Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

        #region Child Installation

        public string insert_location_installation_linkage(string ou_id, string location_id, string ip, string location_prefix)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" insert into location_installation_linkage (child_ouid,child_locationid,childip,child_location_prefix,parent_ouid,parent_locationid,parentip,location_linkage_type) values ('{0}','{1}','{2}','{3}',null,null,null,2)", ou_id, location_id, ip, location_prefix);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" insert into location_installation_linkage (child_ouid,child_locationid,childip,child_location_prefix,parent_ouid,parent_locationid,parentip,location_linkage_type) values ('{0}','{1}','{2}','{3}',null,null,null,2)", ou_id, location_id, ip, location_prefix);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" insert into location_installation_linkage (child_ouid,child_locationid,childip,child_location_prefix,parent_ouid,parent_locationid,parentip,location_linkage_type) values ('{0}','{1}','{2}','{3}',null,null,null,2)", ou_id, location_id, ip, location_prefix);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" insert into location_installation_linkage (child_ouid,child_locationid,childip,child_location_prefix,parent_ouid,parent_locationid,parentip,location_linkage_type) values ('{0}','{1}','{2}','{3}',null,null,null,2)", ou_id, location_id, ip, location_prefix);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_location_installation_linkage Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_child_datatable()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select child_ouid, child_locationid, childip, child_location_prefix from location_installation_linkage where location_linkage_type = 2");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select child_ouid, child_locationid, childip, child_location_prefix from location_installation_linkage where location_linkage_type = 2");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select child_ouid, child_locationid, childip, child_location_prefix from location_installation_linkage where location_linkage_type = 2");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select child_ouid, child_locationid, childip, child_location_prefix from location_installation_linkage where location_linkage_type = 2");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_child_datatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string delete_location_installation_linkage(string ouid, string location_id, string ip, string location_prefix)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" delete from location_installation_linkage where child_ouid = '{0}' and child_locationid = '{1}' and childip = '{2}' and child_location_prefix = '{3}' and location_linkage_type = 2 ", ouid, location_id, ip, location_prefix);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" delete from location_installation_linkage where child_ouid = '{0}' and child_locationid = '{1}' and childip = '{2}' and child_location_prefix = '{3}' and location_linkage_type = 2 ", ouid, location_id, ip, location_prefix);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" delete from location_installation_linkage where child_ouid = '{0}' and child_locationid = '{1}' and childip = '{2}' and child_location_prefix = '{3}' and location_linkage_type = 2 ", ouid, location_id, ip, location_prefix);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" delete from location_installation_linkage where child_ouid = '{0}' and child_locationid = '{1}' and childip = '{2}' and child_location_prefix = '{3}' and location_linkage_type = 2 ", ouid, location_id, ip, location_prefix);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "delete_location_installation_linkage Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        #endregion

        #region External Entitiy 

        public string Get_external_entity_datatable()
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as entityid,em.entity_name as entityname, em.entity_instance_id as entitytypeid, edm.entity_instance_name as entitytype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as entityid,em.entity_name as entityname, em.entity_instance_id as entitytypeid, edm.entity_instance_name as entitytype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 ");
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as entityid,em.entity_name as entityname, em.entity_instance_id as entitytypeid, edm.entity_instance_name as entitytype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 ");
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id as entityid,em.entity_name as entityname, em.entity_instance_id as entitytypeid, edm.entity_instance_name as entitytype, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 ");
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_external_entity_datatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_edit_data(string serialno, string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {1} and serial_no = {0} and active = 1 ", serialno, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {1} and serial_no = {0} and active = 1 ", serialno, entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {1} and serial_no = {0} and active = 1 ", serialno, entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {1} and serial_no = {0} and active = 1 ", serialno, entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_edit_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entity_data(string entityid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id, edm.entity_instance_name, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 and em.entity_id = {0} ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id, edm.entity_instance_name, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 and em.entity_id = {0} ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id, edm.entity_instance_name, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 and em.entity_id = {0} ", entityid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id,em.entity_name, em.entity_instance_id, edm.entity_instance_name, em.active from entity_master em, entity_defination_master edm where em.entity_instance_id = edm.entity_instance_id and em.entity_type_id = 5 and em.active = 1 and em.entity_id = {0} ", entityid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entity_data Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string get_entityname(string entityid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_type_id = 5 and entity_id = {0} and active = 1 ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_type_id = 5 and entity_id = {0} and active = 1 ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select entity_name from entity_master where entity_type_id = 5 and entity_id = {0} and active = 1 ", entityid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select entity_name from entity_master where entity_type_id = 5 and entity_id = {0} and active = 1 ", entityid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "get_entityname Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string Get_entitytype_datatable(string entity_type_id)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select serial_no,entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select serial_no,entity_instance_id,entity_instance_name from entity_defination_master where entity_type_id = {0} and active = 1 ", entity_type_id);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "Get_entitytype_datatable Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entityuserlinkup(string entityid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid, em.entity_name as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, case t.entity_value when isnull(t.entity_value,null) then 1 else 0 end as entity_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 3 and entity_type_id = 5 and active = 1) t on em.entity_id = t.entity_value left outer join entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.active = 1 and em.entity_type_id = 1 and em.entity_id != 1 and em.entity_instance_id = 1063 ", entityid);
                        strQuery.AppendFormat("and em.entity_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_id <> {0} and active = 1) order by userid ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid, em.entity_name as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, case t.entity_value when ifnull(t.entity_value,null) then 1 else 0 end as entity_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 3 and entity_type_id = 5 and active = 1) t on em.entity_id = t.entity_value left outer join entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.active = 1 and em.entity_type_id = 1 and em.entity_id != 1 and em.entity_instance_id = 1063 ", entityid);
                        strQuery.AppendFormat("and em.entity_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_id <> {0} and active = 1) order by userid ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select em.entity_id as userid, em.entity_name as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, case when t.entity_value is null then 1 else 0 end as entity_flag from entity_master em left outer join ");
                        strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 3 and entity_type_id = 5 and active = 1) t on em.entity_id = t.entity_value left outer join entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.active = 1 and em.entity_type_id = 1 and em.entity_id != 1 and em.entity_instance_id = 1063 ", entityid);
                        strQuery.AppendFormat("and em.entity_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_id <> {0} and active = 1) order by userid ", entityid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select em.entity_id as userid, em.entity_name as username, em.entity_instance_id as usertypeid, case edm.entity_instance_name when null then '' else edm.entity_instance_name end as usertype, edm.product_type, ptm.product_type_name, case t.entity_value when ifnull(t.entity_value,null) then 1 else 0 end as entity_flag from entity_master em left outer join ");
                    //    strQuery.AppendFormat("(select entity_value from entity_adhoc_info where entity_id = {0} and entity_adhoc_type_id = 3 and entity_type_id = 5 and active = 1) t on em.entity_id = t.entity_value left outer join entity_defination_master edm on em.entity_instance_id = edm.entity_instance_id left outer join product_type_master ptm on edm.product_type = ptm.product_type_id where em.active = 1 and em.entity_type_id = 1 and em.entity_id != 1 and em.entity_instance_id = 1063 ", entityid);
                    //    strQuery.AppendFormat("and em.entity_id not in (select entity_value from entity_adhoc_info where entity_adhoc_type_id = 3 and entity_id <> {0} and active = 1) order by userid ", entityid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entityuserlinkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string entityoulinkup(string entityid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid, onl.ou_nodelinkage_longname, case t.ou_id when isnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id from ou_entity_relation where entity_id = {0} and entity_type_id = 5 and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 order by onl.ou_nodelinkage_ouid ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid, onl.ou_nodelinkage_longname, case t.ou_id when ifnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id from ou_entity_relation where entity_id = {0} and entity_type_id = 5 and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 order by onl.ou_nodelinkage_ouid ", entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("select onl.ou_nodelinkage_ouid, onl.ou_nodelinkage_longname, case when t.ou_id is null then 0 else 1 end as ou_flag from ou_nodelinkage onl left outer join ");
                        strQuery.AppendFormat("(select ou_id from ou_entity_relation where entity_id = {0} and entity_type_id = 5 and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 order by onl.ou_nodelinkage_ouid ", entityid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("select onl.ou_nodelinkage_ouid, onl.ou_nodelinkage_longname, case t.ou_id when ifnull(t.ou_id,null) then 1 else 0 end as ou_flag from ou_nodelinkage onl left outer join ");
                    //    strQuery.AppendFormat("(select ou_id from ou_entity_relation where entity_id = {0} and entity_type_id = 5 and active = 1) t on onl.ou_nodelinkage_ouid = t.ou_id where onl.active = 1 order by onl.ou_nodelinkage_ouid ", entityid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "entityoulinkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string insert_ou_entity_linkup(string instance_id, string ou_id, string entityid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values (5,{0},{1},{2},0,getdate(),null,1)", instance_id, ou_id, entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values (5,{0},{1},{2},0,now(),null,1)", instance_id, ou_id, entityid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values (5,{0},{1},{2},0,now(),null,1)", instance_id, ou_id, entityid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat("insert into ou_entity_relation (entity_type_id,entity_instance_id,ou_id,entity_id,ou_access,startdate,enddate,active) values (5,{0},{1},{2},0,now(),null,1)", instance_id, ou_id, entityid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "insert_ou_entity_linkup Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }
        public string editentityuserlink(string entity_instanceid, string entityid, string entity_adhoc_typeid)
        {
            StringBuilder strQuery = new StringBuilder();
            try
            {
                switch (dbtype)
                {
                    case OwnYITConstant.DatabaseTypes.MSSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = getdate(), active = 0 where entity_type_id = 5 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, entityid, entity_adhoc_typeid);
                        break;
                    case OwnYITConstant.DatabaseTypes.MYSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 5 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, entityid, entity_adhoc_typeid);
                        break;
                    case OwnYITConstant.DatabaseTypes.PGSQL_SERVER:
                        strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 5 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, entityid, entity_adhoc_typeid);
                        break;
                    //case OwnYITConstant.DatabaseTypes.MARIADB_SERVER:
                    //    strQuery.AppendFormat(" update entity_adhoc_info set enddate = now(), active = 0 where entity_type_id = 5 and entity_instance_id = '{0}' and entity_id = '{1}' and entity_adhoc_type_id = '{2}' and active = 1", entity_instanceid, entityid, entity_adhoc_typeid);
                    //    break;
                }
                //objcommon.WriteLog("DBQueryHandler", "editentityuserlink Query : " + strQuery.ToString());
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("QueryHandler", "log", "QueryHandlerMicroService", "QueryHandler Exception : " + ex.Message.ToString(), true);
            }
            return strQuery.ToString();
        }

        #endregion

    }
}
