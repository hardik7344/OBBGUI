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
    public class ManageRights
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();

        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = "";

        #region Login

        public string loadMenuItem()
        {
            string menuid = "";
            DataTable dt_menu = new DataTable();
            OwnYITConstant.DT_MAIN_MENU = null;
            dt_menu = get_menu_data(Configuration.userid);
            List<string[]> myTable = new List<string[]>();
            foreach (DataRow dr in dt_menu.Rows)
            {
                menuid += "," + dr[0].ToString();
            }
            if (menuid.Trim().Length > 1)
                menuid = menuid.Substring(1);
            if (OwnYITConstant.DT_MAIN_MENU == null)
            {
                OwnYITConstant.DT_MAIN_MENU = GetMainMenu(menuid);
            }
            if (OwnYITConstant.DT_MAIN_MENU.Rows.Count == 0)
            {
                OwnYITConstant.DT_MAIN_MENU = GetMainMenu(menuid);
            }
            return menuid;
        }
        public DataTable Get_Dashbord_chats(string user_id)
        {
            DataTable dtgetdash = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_Dashbord_chats(user_id);
                dtgetdash = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageRights", "Get_Dashbord_chats Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "Get_Dashbord_chatsMicroService", "Get_Dashbord_chats Exception : " + ex.Message.ToString(), true);
            }
            return dtgetdash;
        }
        public DataTable get_menu_data(string userid)
        {
            DataTable dtmenu = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_menu_data(userid);
                dtmenu = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("ManageRights", "log", "get_menu_dataMicroService", "get_menu_data Exception : " + ex.Message.ToString(), true);
            }
            return dtmenu;
        }
        public DataTable GetMainMenu(string menuid)
        {
            DataTable dtmainmenu = new DataTable();
            try
            {
                DatabaseHandler objDB = DBMain.objDBPool.getConnection();
              //  strQuery = objQuery.GetMainMenu(menuid);
                dtmainmenu = objDB.getDatatable(strQuery.ToString());
                DBMain.objDBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageRights", "GetMainMenu Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "GetMainMenuMicroService", "GetMainMenu Exception : " + ex.Message.ToString(), true);
            }
            return dtmainmenu;
        }
        public string get_menu_url(string menuid)
        {
            string menu_url = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_menu_url(menuid);
                menu_url = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageRights", "get_menu_url Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "get_menu_urlMicroService", "get_menu_url Exception : " + ex.Message.ToString(), true);
            }
            return menu_url;
        }

        #endregion

        #region User Type

        public string addusertyperights(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string instance_id = "";
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["menuid"] != null)
                {
                    strQuery = objQuery.get_entity_instance_id_name_usertype("1", display["instance_name"], display["product"]);
                    instance_id = objDB.executeScalar(strQuery.ToString());

                    strQuery1 = objQuery.access_rights_template_cnt("1", instance_id, "0", display["product"]);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                    if (cnt > 0)
                    {
                        strQuery2 = objQuery.access_rights_template_delete("1", instance_id, "0", display["product"]);
                        objDB.execute(strQuery2.ToString());
                    }

                    //menu_id get
                    string menuid = display["menuid"].Substring(1);
                    string[] menu = menuid.Split(',');
                    for (int i = 0; i < menu.Length; i++)
                    {
                        menuid = menu[i].ToString();
                        strQuery3 = objQuery.access_rights_template_insert("1", instance_id, menuid, "0", display["product"]);
                        querystatus = objDB.execute(strQuery3.ToString());
                    }

                    if (querystatus == 1)
                        result = "Rights added successfully.";
                    else
                        result = "Rights added failed.";
                }
                else
                    result = "Please select rights.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageRights", "addusertyperights Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "addusertyperightsMicroService", "addusertyperights Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

        #region Manage User Rights

        public DataTable binduser_groupdata()
        {
            DataTable dtbindusergrp = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_username("1");
                dtbindusergrp = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageRights", "binduser_groupdata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "binduser_groupdataMicroService", "binduser_groupdata Exception : " + ex.Message.ToString(), true);
            }
            return dtbindusergrp;
        }
        public DataTable get_groupname_rights()
        {
            DataTable dtgrpright = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_username("4");
                dtgrpright = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageRights", "get_groupname_rights Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "get_groupname_rightsMicroService", "get_groupname_rights Exception : " + ex.Message.ToString(), true);
            }
            return dtgrpright;
        }
        public DataTable showuserdatatable(string userjson)
        {
            DataTable dtshowuser = new DataTable();
            string str_search = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["userid"] != null)
                {
                    if (display["userid"] != "-1")
                    {
                        str_search += "and userid = '" + display["userid"] + "' ";
                    }
                }
                if (display["groupid"] != null)
                {
                    if (display["groupid"] != "-1")
                    {
                        str_search += "and groupid = '" + display["groupid"] + "' ";
                    }
                }
                strQuery = objQuery.showuserdatatable(str_search);
                dtshowuser = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageRights", "showuserdatatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "showuserdatatableMicroService", "showuserdatatable Exception : " + ex.Message.ToString(), true);
            }
            return dtshowuser;
        }
        public string adduserrights(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["menuid"] != null)
                {
                    string menuid=display["menuid"].Substring(1);
                    strQuery = objQuery.check_user_id(display["userid"]);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                    if (cnt > 0)
                    {
                        strQuery1 = objQuery.delete_user_rights_data_linkuser(display["userid"], "0");
                        objDB.execute(strQuery1.ToString());
                    }
                    strQuery2 = objQuery.Get_startup(display["userid"]);
                    var menu_id = objDB.executeScalar(strQuery2.ToString());
                    strQuery3 = objQuery.access_rights_master_insert(display["userid"], "0", menu_id);
                    objDB.execute(strQuery3.ToString());
                    if (display["userid"] != "null")
                    {
                        if (menuid != "null")
                        {
                            //menu_id get
                            string[] menu = menuid.Split(',');
                            for (int i = 0; i < menu.Length; i++)
                            {
                                menuid = menu[i].ToString();
                                if (menu_id == menuid)
                                { }
                                else
                                {
                                    strQuery4 = objQuery.access_rights_master_insert(display["userid"], "0", menuid);
                                    querystatus = objDB.execute(strQuery4.ToString());
                                }
                            }
                        }
                    }

                    if (querystatus == 1)
                        result = "Rights added successfully.";
                    else
                        result = "Rights added failed.";
                }
                else
                    result = "Please select rights.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageRights", "adduserrights Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageRights", "log", "adduserrightsMicroService", "adduserrights Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

    }
}
