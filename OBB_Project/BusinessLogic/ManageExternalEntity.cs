using OBB_Project.DataAccessLayer;
using OwnYITCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TectonaDatabaseHandlerDLL;

namespace OBB_Project.BusinessLogic
{
    public class ManageExternalEntity
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();
        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = ""; string strQuery5 = ""; string strQuery6 = ""; string strQuery7 = ""; string strQuery8 = ""; string strQuery9 = "";

        #region External Entitiy 

        public DataTable Showexternal_entity()
        {
            DataTable dtexternal = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_external_entity_datatable();
                dtexternal = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "Showexternal_entity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "Showexternal_entityMicroService", "Showexternal_entity Exception : " + ex.Message.ToString(), true);
            }
            return dtexternal;
        }
        public DataTable Get_etype()
        {
            DataTable dtetype = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_type("5", "1");
                dtetype = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "Get_etype Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "Get_etypeMicroService", "Get_etype Exception : " + ex.Message.ToString(), true);
            }
            return dtetype;
        }
        public string Get_entity_type()
        {
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_type("5", "1");
                result = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "Get_entity_type Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "Get_entity_typeMicroService", "Get_entity_type Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string add_external_entity(string userjson)
        {
            string entity_id = "";
            string result = "";
            int querystatus = 0;
            int cnt = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Get_group_count(display["entityname"], "5");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt == 0)
                {
                    strQuery1 = objQuery.get_entity_master_id();
                    entity_id = objDB.executeScalar(strQuery1.ToString());
                    strQuery2 = objQuery.addgroup(display["entitytype"], entity_id, display["entityname"], "", "5");
                    querystatus = objDB.execute(strQuery2.ToString());
                    if (querystatus == 1)
                        result = "External entity added successfully.";
                    else
                        result = "External entity added failed.";
                }
                else
                    result = "External entity already exists.";

                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "add_external_entity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "add_external_entityMicroService", "add_external_entity Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string get_external_entityname(string entityid)
        {
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_entityname(entityid);
                result = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "get_external_entityname Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "get_external_entitynameMicroService", "get_external_entityname Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable entityuserlinkup(string entityid)
        {
            DataTable dteuserlink = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.entityuserlinkup(entityid);
                dteuserlink = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "entityuserlinkup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "entityuserlinkupMicroService", "entityuserlinkup Exception : " + ex.Message.ToString(), true);
            }
            return dteuserlink;
        }
        public string apply_entity_link_user(string userjson)
        {
            var entity_instanceid = "";
            var entity_adhoc_infoid = "";
            var entity_adhoc_typeid = "";
            string result = "";
            int querystatus = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_entity_instance_id_user(display["entityid"], "5");
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                entity_adhoc_typeid = "3";

                strQuery1 = objQuery.editentityuserlink(entity_instanceid, display["entityid"], entity_adhoc_typeid);
                objDB.execute(strQuery1.ToString());

                if (display["user_ids"] != null)
                {
                    string user_ids = display["user_ids"].Substring(1);
                    string[] userid = user_ids.Split(',');
                    for (int i = 0; i < userid.Length; i++)
                    {
                        strQuery2 = objQuery.get_entity_adhoc_info_id();
                        entity_adhoc_infoid = objDB.executeScalar(strQuery2.ToString());

                        strQuery3 = objQuery.addotherinfo(entity_instanceid, entity_adhoc_infoid, display["entityid"], entity_adhoc_typeid, userid[i].ToString(), "5", "1");
                        querystatus = objDB.execute(strQuery3.ToString());
                        if (querystatus == 1)
                            result = "Apply linked successfully.";
                        else
                            result = "Apply linked failed.";
                    }
                }
                else
                {
                    result = "Apply linked successfully.";
                }

                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "apply_entity_link_user Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "apply_entity_link_userMicroService", "apply_entity_link_user Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable GetOUCheckedentity(string entityid)
        {
            DataTable dteoucheck = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.entityoulinkup(entityid);
                dteoucheck = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "GetOUCheckedentity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "GetOUCheckedentityMicroService", "GetOUCheckedentity Exception : " + ex.Message.ToString(), true);
            }
            return dteoucheck;
        }
        public string link_entity_with_ou(string userjson)
        {
            var entity_instanceid = "";
            string result = "";
            int querystatus = 0;
            int cnt = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_entity_instance_id_user(display["entityid"], "5");
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                strQuery1 = objQuery.check_user_exist_ou_linkup("5", entity_instanceid, display["entityid"]);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                if (cnt > 0)
                {
                    strQuery2 = objQuery.ou_entity_relation_update_delete_oulinkup("5", entity_instanceid, display["entityid"]);
                    objDB.execute(strQuery2.ToString());
                }
                if (display["ou_id"] != null)
                {
                    string ou_id = display["ou_id"].Substring(1);
                    string[] ouid = ou_id.Split(',');
                    for (int i = 0; i < ouid.Length; i++)
                    {
                        strQuery3 = objQuery.insert_ou_entity_linkup(entity_instanceid, ouid[i].ToString(), display["entityid"]);
                        querystatus = objDB.execute(strQuery3.ToString());
                    }
                    if (querystatus == 1)
                        result = "Apply linked successfully.";
                    else
                        result = "Apply linked failed.";
                }
                else
                {
                    result = "Apply linked successfully.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "link_entity_with_ou Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "link_entity_with_ouMicroService", "link_entity_with_ou Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable get_entity_datatable(string entityid)
        {
            DataTable dtentity = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_entity_data(entityid);
                dtentity = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "get_entity_datatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "get_entity_datatableMicroService", "get_entity_datatable Exception : " + ex.Message.ToString(), true);
            }
            return dtentity;
        }
        public string editentity(string userjson)
        {
            string result = "";
            int querystatus = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.edituser(display["entitytype"], display["entityid"], display["entityname"]);
                querystatus = objDB.execute(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
                if (querystatus == 1)
                    result = "External entity updated successfully.";
                else
                    result = "External entity updated failed.";
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "editentity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "editentityMicroService", "editentity Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string deleteentitydata(string userjson)
        {
            string result = "";
            int querystatus = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.entity_adhoc_info_update_entity_idwise("5", display["entityid"]);
                objDB.execute(strQuery.ToString());

                strQuery1 = objQuery.deletegroupdata(display["entityid"], "5");
                querystatus = objDB.execute(strQuery1.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
                if (querystatus == 1)
                    result = "External entity deleted successfully.";
                else
                    result = "External entity deleted failed.";
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "deleteentitydata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "deleteentitydataMicroService", "deleteentitydata Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable Get_entitytype_datatable()
        {
            DataTable dtentitytype = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_entitytype_datatable("5");
                dtentitytype = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "Get_entitytype_datatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "Get_entitytype_datatableMicroService", "Get_entitytype_datatable Exception : " + ex.Message.ToString(), true);
            }
            return dtentitytype;
        }
        public string save_entitytype_data(string userjson)
        {
            string instance_id = "";
            string result = "";
            int querystatus = 0;
            int cnt = 0;
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                int id1 = Int16.Parse(display["id"]);
                strQuery = objQuery.entity_defination_master_count("5", display["entity_type"], "1");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt == 0)
                {
                    if (id1 == 1)
                    {
                        strQuery1 = objQuery.get_entity_instance_id_usertype("5", "1");
                        instance_id = objDB.executeScalar(strQuery1.ToString());
                        strQuery2 = objQuery.insert_entity_defination_master("5", instance_id, display["entity_type"], "1");
                        querystatus = objDB.execute(strQuery2.ToString());
                        if (querystatus == 1)
                            result = "Entity type added successfully.";
                        else
                            result = "Entity type added failed.";
                    }
                    else
                    {
                        strQuery3 = objQuery.update_entity_defination_master(display["entity_type"], display["serialno"], "5");
                        querystatus = objDB.execute(strQuery3.ToString());
                        if (querystatus == 1)
                            result = "Entity type updated successfully.";
                        else
                            result = "Entity type updated failed.";
                    }
                }
                else
                {
                    result = "Entity type already exist.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageExternalEntity", "save_entitytype_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "save_entitytype_dataMicroService", "save_entitytype_data Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable get_editdata_entity(string serialno)
        {
            DataTable dteditentity = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_edit_data(serialno, "5");
                dteditentity = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageExternalEntity", "get_editdata_entity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "get_editdata_entityMicroService", "get_editdata_entity Exception : " + ex.Message.ToString(), true);
            }
            return dteditentity;
        }
        public string Delete_entitytype(string userjson)
        {
            string entity_type = "";
            string entity_instance = "";
            string result = "";
            int querystatus = 0;
            int cnt = 0; int cnt1 = 0; int cnt2 = 0; int cnt3 = 0;
            DataTable dtedelete = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.entity_defination_master_entity_typeid_instanceid(display["serialno"]);
                dtedelete = objDB.getDatatable(strQuery.ToString());
                entity_type = dtedelete.Rows[0]["entity_type_id"].ToString();
                entity_instance = dtedelete.Rows[0]["entity_instance_id"].ToString();

                strQuery1 = objQuery.Get_ouLevel_RightsCount(entity_type, entity_instance);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                if (cnt > 0)
                {
                    strQuery2 = objQuery.ou_levelrights_delete_entity_typeid_instanceid(entity_type, entity_instance);
                    objDB.execute(strQuery2.ToString());
                }

                strQuery3 = objQuery.Get_entity_masterCount(entity_type, entity_instance);
                cnt1 = Convert.ToInt16(objDB.executeScalar(strQuery3));
                if (cnt1 > 0)
                {
                    strQuery4 = objQuery.entity_master_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery4.ToString());
                }

                strQuery5 = objQuery.get_entity_adhoc_info_count(entity_type, entity_instance);
                cnt2 = Convert.ToInt16(objDB.executeScalar(strQuery5));
                if (cnt2 > 0)
                {
                    strQuery6 = objQuery.entity_adhoc_info_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery6.ToString());
                }

                strQuery7 = objQuery.Get_ou_entity_relation_count(entity_type, entity_instance);
                cnt3 = Convert.ToInt16(objDB.executeScalar(strQuery7));
                if (cnt3 > 0)
                {
                    strQuery8 = objQuery.ou_entity_relation_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery8.ToString());
                }

                strQuery9 = objQuery.delete_usertype_entity_defination_master(display["serialno"]);
                querystatus = objDB.execute(strQuery9.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);

                if (querystatus == 1)
                    result = "Entity type deleted successfully.";
                else
                    result = "Entity type deleted failed.";
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageExternalEntity", "Delete_entitytype Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageExternalEntity", "log", "Delete_entitytypeMicroService", "Delete_entitytype Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion
    }
}
