using Newtonsoft.Json;
using OBB_Project.DataAccessLayer;
using OBB_Project.Models;
using OwnYITCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TectonaDatabaseHandlerDLL;

namespace OBB_Project.BusinessLogic
{
    public class ManageOrganization
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();

        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = ""; string strQuery5 = ""; string strQuery6 = ""; string strQuery7 = ""; string strQuery8 = ""; string strQuery9 = ""; string strQuery10 = ""; string strQuery11 = ""; string strQuery12 = "";

        public static int Sum(int num1, int num2)
        {
            int total;
            total = num1 + num2;
            return total;
        }

        #region settings_organization_structure

        public string GetTreeDetails1()
        {
            DataTable dttree = new DataTable();
            var treedata = "";
            var childrendata = "";
            var endchardata = "";
            var childdata = " children: [ { ";
            string jsonData11 = "";
            try
            {
                DatabaseHandler objDB = DBMain.objDBPool.getConnection();
                //strQuery = objQuery.Get_treedata();
                dttree = objDB.getDatatable(strQuery.ToString());

                for (var i = 0; i < dttree.Rows.Count; i++)
                {
                    if (dttree.Rows[i]["ou_nodelinkage_levelid"].ToString() == "1")
                        treedata += " id: '1', text: '" + dttree.Rows[i]["ou_nodelinkage_nodename"].ToString() + "' , a_attr: { href: '#' }, state: {  selected: false },";
                    else if (dttree.Rows[i]["ou_nodelinkage_levelid"].ToString() == "2")
                        childrendata += childdata + " id: '" + dttree.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dttree.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },";
                    else
                        childrendata += childdata + " id: '" + dttree.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dttree.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },},]";
                }

                endchardata = treedata + childrendata + " }, ] }, ] ; ";
                jsonData11 = "[ { " + endchardata;
                DBMain.objDBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "GetTreeDetails1 Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "GetTreeDetails1MicroService", "GetTreeDetails1 Exception : " + ex.Message.ToString(), true);
            }
            return jsonData11;
        }
        public DataTable Get_Level()
        {
            DataTable dtlevel = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_Level();
                dtlevel = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Get_Level Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Get_LevelMicroService", "Get_Level Exception : " + ex.Message.ToString(), true);
            }

            return dtlevel;
        }
        public DataTable Get_Level_id()
        {
            DataTable dtlevelid = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_Level_id();
                dtlevelid = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Get_Level_id Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Get_Level_idMicroService", "Get_Level_id Exception : " + ex.Message.ToString(), true);
            }
            return dtlevelid;
        }
        public string insert_oulevel(string userjson)
        {
            Int64 cnt = 0;
            Int32 querystatus = 0;

            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");

                string strQuery = objQuery.entity_defination_master_count("0", display["level"], "1");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt == 0)
                {
                    string strQuery1 = objQuery.insert_entity_defination_master("0", display["id"], display["level"], "1");
                    querystatus = objDB.execute(strQuery1);
                    if (querystatus == 1)
                        result = "Level saved successfully.";
                    else
                        result = "Level saved failed.";
                }
                else
                    result = "Level alredy exists.";

                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "insert_oulevel Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "insert_oulevelMicroService", "insert_oulevel Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string Update_level(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                //propertyname = propertyname.Trim().ToString();

                //if (id != null && propertyname != null)
                //{
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.entity_defination_master_count("0", display["propertyname"], "1");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt == 0)
                {
                    strQuery1 = objQuery.Update_level(display["levelid"], display["propertyname"]);
                    querystatus = objDB.execute(strQuery1.ToString());
                    if (querystatus == 1)
                        result = "Level updated successfully.";
                    else
                        result = "Level updated failed.";
                }
                else
                    result = "Level alredy exists.";
                //}
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Update_level Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Update_levelMicroService", "Update_level Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable Get_level_rights(string levelid)
        {
            DataTable dtlevelrights = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_level_rights(levelid);
                dtlevelrights = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Get_level_rights Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Get_level_rightsMicroService", "Get_level_rights Exception : " + ex.Message.ToString(), true);
            }
            return dtlevelrights;
        }
        public int Get_Level_RightsCount(string entity_type_id, string entity_instance_id, string levelid)
        {
            int cnt = 0;
            try
            {
                DatabaseHandler objDB = DBMain.objDBPool.getConnection();
                //strQuery = objQuery.Get_Level_RightsCount(entity_type_id, entity_instance_id, levelid);
                //cnt = objDB.getIntValue(strQuery.ToString());
                DBMain.objDBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Get_Level_RightsCount Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Get_Level_RightsCountMicroService", "Get_Level_RightsCount Exception : " + ex.Message.ToString(), true);
            }
            return cnt;
        }
        public string Insert_Updatelevelrights(string userjson)
        {
            int querystatus = 0;
            string result = "";
            DataTable dteinstance = new DataTable();

            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");

                if (display["propertyid"] == null)
                {
                    strQuery = objQuery.ou_levelrights_delete(display["levelid"]);
                    querystatus = objDB.execute(strQuery.ToString());
                }
                else
                {
                    string propertyid = display["propertyid"].Substring(1);
                    if (display["levelid"] != null)
                    {
                        strQuery = objQuery.entity_type_id_instance_id(display["propertyid"]);
                        dteinstance = objDB.getDatatable(strQuery.ToString());

                        foreach (DataRow dr in dteinstance.Rows)
                        {
                            if (Get_Level_RightsCount(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), display["levelid"]) > 0)
                            {
                                strQuery1 = objQuery.ou_levelrights_delete(display["levelid"]);
                                objDB.execute(strQuery1.ToString());

                                strQuery2 = objQuery.insert_level_rights(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), display["levelid"]);
                                querystatus = objDB.execute(strQuery2.ToString());
                            }
                            else
                            {
                                strQuery3 = objQuery.insert_level_rights(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), display["levelid"]);
                                querystatus = objDB.execute(strQuery3.ToString());
                            }
                        }
                    }
                }
                if (querystatus == 1)
                    result = "Property saved successfully.";
                else
                    result = "Property saved failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Insert_Updatelevelrights Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Insert_UpdatelevelrightsMicroService", "Insert_Updatelevelrights Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string Get_OU_Child_Data_Tree()
        {
            string nodeslist = "";
            DataTable dtoutree = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_OU_Child_Data_Tree();
                dtoutree = objDB.getDatatable(strQuery.ToString());
                List<TreeNode> nodes = new List<TreeNode>();

                if (dtoutree != null && dtoutree.Rows.Count > 0)
                {
                    foreach (DataRow row in dtoutree.Rows)
                    {
                        nodes.Add(new TreeNode { id = row["ou_nodelinkage_ouid"].ToString(), text = row["ou_nodelinkage_nodename"].ToString(), parent = row["ou_nodelinkage_parentouid"].ToString(), active = row["active"].ToString() });
                    }
                    nodeslist = JsonConvert.SerializeObject(nodes);
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Get_OU_Child_Data_Tree Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Get_OU_Child_Data_TreeMicroService", "Get_OU_Child_Data_Tree Exception : " + ex.Message.ToString(), true);
            }
            return nodeslist;
        }
        public DataTable Add_Branch_Unit_ou_data(string userjson)
        {
            DataTable dtaddou = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["ou_id"] == "0")
                {
                    strQuery = objQuery.get_root_ouid();
                    display["ou_id"] = objDB.executeScalar(strQuery.ToString());
                }
                strQuery1 = objQuery.Get_OU_self_Data(display["ou_id"]);
                dtaddou = objDB.getDatatable(strQuery1.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Add_Branch_Unit_ou_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "Add_Branch_Unit_ou_dataMicroService", "Add_Branch_Unit_ou_data Exception : " + ex.Message.ToString(), true);
            }
            return dtaddou;
        }
        public string add_new_ou_branch_unit(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            long maxouid;
            int levelid;
            int entity_levelid;
            Int64 parent_ouid;
            DataTable dtouadd = new DataTable();
            DataTable dtparentouid = new DataTable();
            DataTable dtouallchild = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                //oulevelid = Sum(oulevelid, 1);
                //ou_name = ou_name.Trim().ToString();

                strQuery1 = objQuery.get_levelid_of_ouid(display["ou_id"]);
                levelid = Convert.ToInt16(objDB.executeScalar(strQuery1));

                strQuery2 = objQuery.get_entity_levelid();
                entity_levelid = Convert.ToInt16(objDB.executeScalar(strQuery2));

                if (entity_levelid > levelid)
                {
                    strQuery3 = objQuery.get_ouname_oulevel_wise_count(display["ou_id"], Convert.ToInt32(display["oulevelid"]), display["ou_name"]);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery3));
                    if (cnt == 0)
                    {
                        strQuery = objQuery.get_max_ouid();
                        //maxouid = objDB.getLongValue(strQuery.ToString());
                        maxouid = Convert.ToInt64(objDB.executeScalar(strQuery.ToString()));

                        strQuery4 = objQuery.insert_new_ou_branch_unit(maxouid.ToString(), display["ou_id"], Convert.ToString(levelid + 1), levelid.ToString(), display["ou_name"]);
                        querystatus = objDB.execute(strQuery4.ToString());

                        strQuery5 = objQuery.ou_allchild_insert(display["ou_id"], maxouid.ToString());
                        objDB.execute(strQuery5.ToString());

                        parent_ouid = Convert.ToInt64(display["ou_id"]);

                        strQuery6 = objQuery.ou_nodelinkage_ouid_parentouid();
                        dtouadd = objDB.getDatatable(strQuery6.ToString());

                        foreach (DataRow row in dtouadd.Rows)
                        {
                            strQuery7 = objQuery.ou_nodelinkage_parentouid(parent_ouid.ToString());
                            //parent_ouid = objDB.getLongValue(strQuery7.ToString());
                            parent_ouid = Convert.ToInt64(objDB.executeScalar(strQuery7.ToString()));

                            if (parent_ouid > 0)
                            {
                                strQuery8 = objQuery.ou_allchild_insert(parent_ouid.ToString(), maxouid.ToString());
                                objDB.execute(strQuery8.ToString());
                            }
                            else
                                break;
                        }

                        strQuery9 = objQuery.location_installation_linkage_child();
                        string locationid = objDB.executeScalar(strQuery9.ToString());

                        strQuery10 = objQuery.native_oulinkage_insert(locationid, maxouid.ToString());
                        objDB.execute(strQuery10.ToString());

                        strQuery11 = objQuery.execute_inslongname();
                        objDB.execute(strQuery11.ToString());

                        //for nocdesk project self vendor(external entity) linkup with new created ou
                        strQuery12 = objQuery.insert_ou_entity_linkup("5001", maxouid.ToString(), "5");
                        objDB.execute(strQuery12.ToString());

                        if (querystatus == 1)
                            result = "OU saved successfully.";
                        else
                            result = "OU saved failed.";
                    }
                    else //OU Node name same but ou root has differnt that time this(else coding) code use (20201211)
                    {
                        result = "OU name already exist.";
                    }
                }
                else
                {
                    result = "No more level available.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "add_new_ou_branch_unit Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "add_new_ou_branch_unitMicroService", "add_new_ou_branch_unit Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable ousystemlinkup(string userjson)
        {
            DataTable dtsystemlink = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.ousystemlinkup(display["ou_id"]);
                dtsystemlink = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "ousystemlinkup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "ousystemlinkupMicroService", "ousystemlinkup Exception : " + ex.Message.ToString(), true);
            }
            return dtsystemlink;
        }
        public string apply_ou_link_system(string userjson)
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
                strQuery = objQuery.get_entity_instance_id_device("2");
                entity_instanceid = objDB.executeScalar(strQuery.ToString());

                entity_adhoc_typeid = "3";

                strQuery1 = objQuery.editousystemlink(entity_instanceid, display["ouid"], entity_adhoc_typeid);
                objDB.execute(strQuery1.ToString());

                if (display["device_ids"] != null && display["device_ids"] != "")
                {
                    string device_ids = display["device_ids"].Substring(1);
                    string[] deviceid = device_ids.Split(',');
                    for (int i = 0; i < deviceid.Length; i++)
                    {
                        strQuery2 = objQuery.get_entity_adhoc_info_id();
                        entity_adhoc_infoid = objDB.executeScalar(strQuery2.ToString());

                        strQuery3 = objQuery.addotherinfo(entity_instanceid, entity_adhoc_infoid, display["ouid"], entity_adhoc_typeid, deviceid[i].ToString(), "2", "1");
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
                //objcommon.WriteLog("ManageOrganization", "apply_ou_link_system Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "apply_ou_link_systemMicroService", "apply_ou_link_system Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string Updateoudetails(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                string ouname = display["ouname"].Trim().ToString();
                if (display["ouid"] != null && ouname != null)
                {
                    strQuery = objQuery.get_ou_name_count(display["ouname"]);
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                    if (cnt == 0)
                    {
                        strQuery1 = objQuery.Update_OUname(display["ouid"], display["ouname"]);
                        querystatus = objDB.execute(strQuery1.ToString());

                        strQuery2 = objQuery.execute_inslongname();
                        objDB.execute(strQuery2.ToString());

                        if (querystatus == 1)
                            result = "OU Updated successfully.";
                        else
                            result = "OU Updated failed.";
                    }
                    else
                        result = "OU name already exist.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "Updateoudetails Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "UpdateoudetailsMicroService", "Updateoudetails Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string DeleteOU(string userjson)
        {
            int cnt = 0; int cnt1 = 0; int cnt2 = 0; int cnt3 = 0;
            int querystatus = 0;
            string result = "";
            string ou_id = "";
            DataTable dtoudelete = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["ouid"] != null)
                {
                    if (display["ouid"] != "-1")
                    {
                        strQuery = objQuery.get_ou_nodelinkage_ouid(display["ouid"]);
                        dtoudelete = objDB.getDatatable(strQuery.ToString());

                        if (dtoudelete.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtoudelete.Rows.Count; i++)
                            {
                                ou_id = dtoudelete.Rows[i]["ou_nodelinkage_ouid"].ToString();

                                strQuery1 = objQuery.ou_entity_relation_count(display["ou_id"]);
                                cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                                if (cnt > 0)
                                {
                                    strQuery2 = objQuery.ou_entity_relation_update_ouidwise(display["ou_id"]);
                                    objDB.execute(strQuery2.ToString());
                                }
                                strQuery3 = objQuery.entity_adhoc_info_count_ou(display["ou_id"]);
                                cnt1 = Convert.ToInt16(objDB.executeScalar(strQuery3));
                                if (cnt1 > 0)
                                {
                                    strQuery4 = objQuery.entity_adhoc_info_update_entity_idwise("0", display["ou_id"]);
                                    objDB.execute(strQuery4.ToString());
                                }
                                strQuery5 = objQuery.native_oulinkage_delete(display["ou_id"]);
                                objDB.execute(strQuery5.ToString());

                                strQuery6 = objQuery.Delete_OU(ou_id);
                                querystatus = objDB.execute(strQuery6.ToString());
                            }
                        }

                        strQuery7 = objQuery.ou_entity_relation_count(display["ouid"]);
                        cnt2 = Convert.ToInt16(objDB.executeScalar(strQuery7));
                        if (cnt2 > 0)
                        {
                            strQuery8 = objQuery.ou_entity_relation_update_ouidwise(display["ouid"]);
                            objDB.execute(strQuery8.ToString());
                        }
                        strQuery9 = objQuery.entity_adhoc_info_count_ou(display["ouid"]);
                        cnt3 = Convert.ToInt16(objDB.executeScalar(strQuery9));
                        if (cnt3 > 0)
                        {
                            strQuery10 = objQuery.entity_adhoc_info_update_entity_idwise("0", display["ouid"]);
                            objDB.execute(strQuery10.ToString());
                        }
                        strQuery11 = objQuery.native_oulinkage_delete(display["ouid"]);
                        objDB.execute(strQuery11.ToString());

                        strQuery12 = objQuery.Delete_OU(display["ouid"]);
                        querystatus = objDB.execute(strQuery12.ToString());

                        if (querystatus == 1)
                            result = "OU deleted successfully.";
                        else
                            result = "OU deleted failed.";
                    }
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOrganization", "DeleteOU Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOrganization", "log", "DeleteOUMicroService", "DeleteOU Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

    }
}
