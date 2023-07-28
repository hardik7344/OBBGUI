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
    public class ManageOtherInformation
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();

        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = ""; string strQuery5 = "";

        #region settings_organization_structure__other information

        public DataTable get_parameterdata()
        {
            DataTable dtparm = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_ouparameterdata();
                dtparm = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOtherInformation", "get_parameterdata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "get_parameterdataMicroService", "get_parameterdata Exception : " + ex.Message.ToString(), true);
            }
            return dtparm;
        }
        public string paramadd(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Get_adhoc_count(display["parameter"]);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery)); 
                if (cnt <= 0)
                {
                    strQuery1 = objQuery.ouparamadd(display["parameter"]);
                    querystatus = objDB.execute(strQuery1.ToString());
                    result = "Parameter added successfully";
                }
                else
                    result = "Parameter already exists.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageOtherInformation", "paramadd Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "paramaddMicroService", "paramadd Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string otherinfo(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            var entity_instanceid = "";
            var entity_adhoc_infoid = "";
            var entity_adhoc_typeid = "";

            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_levelid_of_ouid(display["ouid"]);
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                strQuery1 = objQuery.get_entity_adhoc_type_id(display["parameter"]);
                entity_adhoc_typeid = objDB.executeScalar(strQuery1.ToString());

                strQuery2 = objQuery.Get_otherinfo_count(entity_instanceid, display["ouid"], entity_adhoc_typeid, "0");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery2.ToString()));

                if (cnt == 0)
                {
                    strQuery3 = objQuery.get_entity_adhoc_info_id();
                    entity_adhoc_infoid = objDB.executeScalar(strQuery3.ToString());

                    strQuery4 = objQuery.addotherinfo(entity_instanceid, entity_adhoc_infoid, display["ouid"], entity_adhoc_typeid, display["parmvalue"], "0", "1");
                    querystatus = objDB.execute(strQuery4.ToString());
                    if (querystatus == 1)
                        result = "Other information added successfully.";
                    else
                        result = "Other information added failed.";
                }
                else
                {
                    strQuery5 = objQuery.editotherinfo(entity_instanceid, display["ouid"], entity_adhoc_typeid, display["parmvalue"], "0");
                    querystatus = objDB.execute(strQuery5.ToString());
                    if (querystatus == 1)
                        result = "Other information updated successfully.";
                    else
                        result = "Other information updated failed.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOtherInformation", "otherinfo Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "otherinfoMicroService", "otherinfo Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable show_otherinfo(string userjson)
        {
            DataTable dtinfo = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Get_otherinfo(display["ouid"]);
                dtinfo = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOtherInformation", "show_otherinfo Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "show_otherinfoMicroService", "show_otherinfo Exception : " + ex.Message.ToString(), true);
            }
            return dtinfo;
        }
        public DataTable get_editotherinfodata(string userjson)
        {
            DataTable dteditinfo = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_editotherinfo(display["serialno"]);
                dteditinfo = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOtherInformation", "get_editotherinfodata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "get_editotherinfodataMicroService", "get_editotherinfodata Exception : " + ex.Message.ToString(), true);
            }
            return dteditinfo;
        }
        public string Deleteinfo(string userjson)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Delete_info(display["serial_no"]);
                querystatus = objDB.execute(strQuery.ToString());
                if (querystatus == 1)
                    result = "Other information deleted successfully.";
                else
                    result = "Other information deleted failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOtherInformation", "Deleteinfo Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "DeleteinfoMicroService", "Deleteinfo Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

        #region settings_user_mgmt

        public string otherinfo_user(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            var entity_instanceid = "";
            var entity_adhoc_infoid = "";
            var entity_adhoc_typeid = "";

            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_entity_instance_id_user(display["userid"], "1");
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                strQuery1 = objQuery.get_entity_adhoc_type_id(display["parameter"]);
                entity_adhoc_typeid = objDB.executeScalar(strQuery1.ToString());

                strQuery2 = objQuery.Get_otherinfo_count(entity_instanceid, display["userid"], entity_adhoc_typeid, "1");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery2)); 
                if (cnt == 0)
                {
                    strQuery3 = objQuery.get_entity_adhoc_info_id();
                    entity_adhoc_infoid = objDB.executeScalar(strQuery3.ToString());

                    strQuery4 = objQuery.addotherinfo(entity_instanceid, entity_adhoc_infoid, display["userid"], entity_adhoc_typeid, display["parmvalue"], "1", display["product"]);
                    querystatus = objDB.execute(strQuery4.ToString());
                    if (querystatus == 1)
                        result = "User information added successfully";
                    else
                        result = "User information added failed";
                }
                else
                {
                    strQuery5 = objQuery.editotherinfo(entity_instanceid, display["userid"], entity_adhoc_typeid, display["parmvalue"], "1");
                    querystatus = objDB.execute(strQuery5.ToString());
                    if (querystatus == 1)
                        result = "User information updated successfully";
                    else
                        result = "User information updated failed";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOtherInformation", "otherinfo_user Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "otherinfo_userMicroService", "otherinfo_user Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable show_otherinfo_user(string userid)
        {
            DataTable dtuserinfo = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_otherinfo_user(userid, "1");
                dtuserinfo = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOtherInformation", "show_otherinfo_user Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "show_otherinfo_userMicroService", "show_otherinfo_user" +
                    "" +
                    "" +
                    " Exception : " + ex.Message.ToString(), true);
            }
            return dtuserinfo;
        }

        #endregion

        #region External Entity

        public string otherinfo_entity(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            var entity_instanceid = "";
            var entity_adhoc_infoid = "";
            var entity_adhoc_typeid = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_entity_instance_id_user(display["entityid"], "5");
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                strQuery1 = objQuery.get_entity_adhoc_type_id(display["parameter"]);
                entity_adhoc_typeid = objDB.executeScalar(strQuery1.ToString());

                strQuery2 = objQuery.Get_otherinfo_count(entity_instanceid, display["entityid"], entity_adhoc_typeid, "5");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery2));
                if (cnt == 0)
                {
                    strQuery3 = objQuery.get_entity_adhoc_info_id();
                    entity_adhoc_infoid = objDB.executeScalar(strQuery3.ToString());

                    strQuery4 = objQuery.addotherinfo(entity_instanceid, entity_adhoc_infoid, display["entityid"], entity_adhoc_typeid, display["parmvalue"], "5", "1");
                    querystatus = objDB.execute(strQuery4.ToString());
                    if (querystatus == 1)
                        result = "External Entity information added successfully";
                    else
                        result = "External Entity information added failed";
                }
                else
                {
                    strQuery5 = objQuery.editotherinfo(entity_instanceid, display["entityid"], entity_adhoc_typeid, display["parmvalue"], "5");
                    querystatus = objDB.execute(strQuery5.ToString());
                    if (querystatus == 1)
                        result = "External Entity information updated successfully";
                    else
                        result = "External Entity information updated failed";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOtherInformation", "otherinfo_entity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "otherinfo_entityMicroService", "otherinfo_entity Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable show_otherinfo_entity(string entityid)
        {
            DataTable dteinfo = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_otherinfo_user(entityid, "5");
                dteinfo = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageOtherInformation", "show_otherinfo_entity Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOtherInformation", "log", "show_otherinfo_entityMicroService", "show_otherinfo_entity Exception : " + ex.Message.ToString(), true);
            }
            return dteinfo;
        }

        #endregion

    }
}
