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
    public class ManageUser
    {
        QueryHandler objQuery = new QueryHandler();
        //  OwnYITCommon objcommon = new OwnYITCommon();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();
        int cnt = 0;
        string userid = "";
        string resultdata = "";
        string JSONresult = string.Empty;
        DataTable resultdt = new DataTable();
        DataTable dt = new DataTable();
        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = ""; string strQuery5 = ""; string strQuery6 = ""; string strQuery7 = ""; string strQuery8 = ""; string strQuery9 = ""; string strQuery10 = ""; string strQuery11 = ""; string strQuery12 = "";

        #region Login

        public bool doUserLogin(string username, string password)
        {
            bool bFlag = false;
            Int64 retValue = 0;
            try
            {
                //string strTemp = dtconversion.Base64Decode(userjson);
                //IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                //string[] strArray = { "username", "password" };
                //IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString_base64(userjson, strArray);
                string Password = Encrypt.EncryptText(password);
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.doUserLogin(username, Password);
                retValue = Convert.ToInt64(objDB.executeScalar(strQuery));
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {

                objcommon.WriteLog("OBBLogin", "log", "LoginMicroService", "doUserLogin Exception : " + ex.Message.ToString(), true);
            }
            if (retValue > 0)
                bFlag = true;
            return bFlag;
        }


        #endregion

        #region User Management 

        public string get_user_id(string username)
        {
            string user_id = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_user_id(username);
                user_id = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "get_user_id Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "get_user_idMicroService", "get_user_id Exception : " + ex.Message.ToString(), true);
            }
            return user_id;
        }
        public string Get_startup(string userid)
        {
            string entity_value = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_startup(userid);
                entity_value = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "Get_startup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "Get_startupMicroService", "Get_startup Exception : " + ex.Message.ToString(), true);
            }
            return entity_value;
        }
        public DataTable ShowUsermgmt()
        {
            DataTable dtuser = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_Usermgmt();
                dtuser = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "ShowUsermgmt Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "ShowUsermgmtMicroService", "ShowUsermgmt Exception : " + ex.Message.ToString(), true);
            }
            return dtuser;
        }
        public DataTable Get_user_type(string product)
        {
            DataTable dtusertype = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(product);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                string strProduct = display["product_type"];
                if (strProduct == "")
                    strProduct = display["product"];
                strQuery = objQuery.get_type("1", strProduct);
                dtusertype = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageUser", "Get_user_type Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "Get_user_typeMicroService", "Get_user_type Exception : " + ex.Message.ToString(), true);
            }
            return dtusertype;
        }
        public DataTable Get_startup_page(string product_type)
        {
            DataTable dtstartup = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(product_type);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                string strProduct = display["product_type"];
                if(strProduct=="")
                    strProduct = display["product"];
                
                strQuery = objQuery.Get_startup_page(strProduct);
                dtstartup = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "Get_startup_page Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "Get_startup_pageMicroService", "Get_startup_page Exception : " + ex.Message.ToString(), true);
            }
            return dtstartup;
        }
        public string adduser(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            var entity_id = "";
            var adhoc_infoid = "";
            var adhoc_infoid_startscreen = "";
            string instance_id = "";
            string startup = "";
            string encryptpassword = "";
            string result = "";
            DataTable dtmenuid = new DataTable();
            string strTemp = dtconversion.Base64Decode(userjson);
            IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
            string username = display["username"].ToLower();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                if ((!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(username)) == true)
                {
                    encryptpassword = Encrypt.EncryptText(display["userpassword"]);
                }
                if (username != "" && display["usertype"] != "")
                {
                    instance_id = display["usertype"];

                    strQuery = objQuery.Get_user_count(username, "1", instance_id);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                    if (cnt == 0)
                    {
                        strQuery1 = objQuery.get_entity_master_id();
                        entity_id = objDB.executeScalar(strQuery1.ToString());

                        strQuery2 = objQuery.get_entity_adhoc_info_id();
                        adhoc_infoid = objDB.executeScalar(strQuery2.ToString());

                        strQuery3 = objQuery.addgroup(display["usertype"], entity_id, username, "", "1");
                        querystatus = objDB.execute(strQuery3.ToString());

                        //password save
                        strQuery4 = objQuery.entity_adhoc_info_user_password(display["usertype"], adhoc_infoid, entity_id, encryptpassword, display["product"]);
                        objDB.execute(strQuery4.ToString());

                        //StartUp Screen save
                        strQuery5 = objQuery.get_entity_adhoc_info_id();
                        adhoc_infoid_startscreen = objDB.executeScalar(strQuery5.ToString());

                        strQuery6 = objQuery.access_rights_template_startup_menuid("1", display["usertype"], "1", display["product"]);
                        startup = objDB.executeScalar(strQuery6.ToString());

                        strQuery7 = objQuery.entity_adhoc_info_user_startupscreen(display["usertype"], adhoc_infoid_startscreen, entity_id, startup, display["product"]);
                        objDB.execute(strQuery7.ToString());

                        strQuery8 = objQuery.access_rights_template_menuid("1", display["usertype"], display["product"]);
                        dtmenuid = objDB.getDatatable(strQuery8.ToString());
                        for (int i = 0; i < dtmenuid.Rows.Count; i++)
                        {
                            strQuery9 = objQuery.access_rights_master_insert(entity_id, "0", dtmenuid.Rows[i]["menu_id"].ToString());
                            objDB.execute(strQuery9.ToString());
                        }

                        if (querystatus == 1)
                            result = "User added successfully.";
                        else
                            result = "User added failed.";
                    }
                    else
                        result = "User already exists.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "adduser Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "adduserMicroService", "adduser Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable get_user_data(string userjson)
        {
            DataTable dtgetuser = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_user_data(display["userid"]);
                dtgetuser = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "get_user_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "get_user_dataMicroService", "get_user_data Exception : " + ex.Message.ToString(), true);
            }
            return dtgetuser;
        }
        public string get_user_password(string userjson)
        {
            string decryptpassword = "";
            string userpassword = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_password(display["userid"]);
                userpassword = objDB.executeScalar(strQuery.ToString());
                decryptpassword = Encrypt.DecryptText(userpassword);
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "get_user_password Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "get_user_passwordMicroService", "get_user_password Exception : " + ex.Message.ToString(), true);
            }
            return decryptpassword;
        }
        public string edituser(string userjson)
        {
            int cnt = 0; int cnt1 = 0;
            int querystatus = 0;
            var adhoc_infoid = "";
            var adhoc_infoid_startscreen = "";
            string startup = "";
            string encryptpassword = "";
            string result = "";
            string product_id = "";
            DataTable dtmenu_id = new DataTable();
            string strTemp = dtconversion.Base64Decode(userjson);
            IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
            string username = display["username"].ToLower();
            if ((!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(username)) == true)
            {
                try
                {
                    encryptpassword = Encrypt.EncryptText(display["userpassword"]);
                }
                catch (Exception) { }
            }
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                if (username != "" && display["usertype"] != "")
                {
                    strQuery12 = objQuery.Get_product_type_id(display["product"]);
                    product_id = objDB.executeScalar(strQuery12.ToString());

                    strQuery = objQuery.get_edit_entity_adhoc_info_id(display["userid"]);
                    adhoc_infoid = objDB.executeScalar(strQuery.ToString());

                    strQuery1 = objQuery.get_edit_entity_adhoc_info_id_startscreen(display["userid"]);
                    adhoc_infoid_startscreen = objDB.executeScalar(strQuery1.ToString());

                    strQuery2 = objQuery.edituser(display["usertype"], display["userid"], username);
                    querystatus = objDB.execute(strQuery2.ToString());

                    //password save
                    strQuery3 = objQuery.entity_adhoc_info_user_password_update(display["usertype"], adhoc_infoid, display["userid"], encryptpassword);
                    objDB.execute(strQuery3.ToString());

                    //StartUp Screen save
                    strQuery4 = objQuery.access_rights_template_startup_menuid("1", display["usertype"], "1", product_id);
                    startup = objDB.executeScalar(strQuery4.ToString());

                    strQuery5 = objQuery.entity_adhoc_info_user_startupscreen_update(display["usertype"], display["userid"], startup);
                    objDB.execute(strQuery5.ToString());

                    strQuery6 = objQuery.access_rights_master_count(display["userid"]);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery6));
                    if (cnt > 0)
                    {
                        strQuery7 = objQuery.delete_user_rights_data(display["userid"]);
                        objDB.execute(strQuery7.ToString());
                    }

                    strQuery8 = objQuery.access_rights_template_menuid("1", display["usertype"], product_id);
                    dtmenu_id = objDB.getDatatable(strQuery8.ToString());
                    for (int i = 0; i < dtmenu_id.Rows.Count; i++)
                    {
                        strQuery9 = objQuery.access_rights_master_insert(display["userid"], "0", dtmenu_id.Rows[i]["menu_id"].ToString());
                        objDB.execute(strQuery9.ToString());
                    }

                    strQuery10 = objQuery.ou_entity_relation_count_user(display["userid"]);
                    cnt1 = Convert.ToInt16(objDB.executeScalar(strQuery10));
                    if (cnt1 > 0)
                    {
                        strQuery11 = objQuery.ou_entity_relation_update_user(display["userid"], display["usertype"]);
                        objDB.execute(strQuery11.ToString());
                    }

                    if (querystatus == 1)
                        result = "User updated successfully.";
                    else
                        result = "User updated failed.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "edituser Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "edituserMicroService", "edituser Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string set_reset_password(string userjson)
        {
            int cnt = 0; int cnt1 = 0;
            string encryptpasswoed = "";
            string strreturn = "";
            string passwordencrypt = "";
            string instance_id = "";
            string adhoc_infoid = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_password(display["userid"]);
                passwordencrypt = objDB.executeScalar(strQuery.ToString());

                strQuery1 = objQuery.set_reset_check_old_password(passwordencrypt, display["userid"]);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                if (cnt == -1)
                {
                    strreturn = "Old password is incorrect.";
                }
                else if (cnt == -2)
                {
                    strreturn = "Some problem while set reset password.";
                }
                else if (cnt == 1)
                {
                    strQuery2 = objQuery.get_entity_instance_id_user(display["userid"], "1");
                    instance_id = objDB.executeScalar(strQuery2.ToString());

                    strQuery3 = objQuery.get_entity_adhoc_info_id();
                    adhoc_infoid = objDB.executeScalar(strQuery3.ToString());

                    encryptpasswoed = Encrypt.EncryptText(display["newpassword"]);
                    strQuery4 = objQuery.set_reset_password(display["userid"]);
                    objDB.executeScalar(strQuery4);

                    strQuery5 = objQuery.entity_adhoc_info_user_password(instance_id, adhoc_infoid, display["userid"], encryptpasswoed, display["product"]);
                    cnt1 = objDB.execute(strQuery5.ToString());

                    if (cnt1 == 1)
                        strreturn = "Password set successfully.";
                    else
                        strreturn = "Password set failed.";
                }
                else
                {
                    strreturn = "Please enter valid password.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "set_reset_password Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "set_reset_passwordMicroService", "set_reset_password Exception : " + ex.Message.ToString(), true);
            }
            return strreturn;
        }
        public string deleteuserdata(string userjson)
        {
            int cnt = 0; int cnt1 = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.ou_entity_relation_count_user(display["userid"]);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt > 0)
                {
                    strQuery1 = objQuery.ou_entity_relation_update_delete_user(display["userid"]);
                    objDB.execute(strQuery1.ToString());
                }

                strQuery2 = objQuery.entity_adhoc_info_update_entity_idwise("1", display["userid"]);
                objDB.execute(strQuery2.ToString());

                strQuery3 = objQuery.access_rights_master_count_user(display["userid"]);
                cnt1 = Convert.ToInt16(objDB.executeScalar(strQuery3));
                if (cnt1 > 0)
                {
                    strQuery4 = objQuery.delete_user_rights_data(display["userid"]);
                    objDB.execute(strQuery4.ToString());
                }

                strQuery5 = objQuery.deletegroupdata(display["userid"], "1");
                querystatus = objDB.execute(strQuery5.ToString());
                if (querystatus == 1)
                    result = "User deleted successfully.";
                else
                    result = "User deleted failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageUser", "deleteuserdata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "deleteuserdataMicroService", "deleteuserdata Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string old_password_match(string userjson)
        {
            string result = "";
            string encryptpasswoed = "";
            string passwordencrypt = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.get_password(display["user_id"]);
                encryptpasswoed = objDB.executeScalar(strQuery.ToString());

                passwordencrypt = Encrypt.DecryptText(encryptpasswoed);
                if (display["oldpassword"] != passwordencrypt)
                {
                    result = "Please enter valid old password.";
                }
                else
                    result = "Password is correct!!!";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageUser", "old_password_match Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageUser", "log", "old_password_matchMicroService", "old_password_match Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion
    }

}
