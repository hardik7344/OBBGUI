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
    public class ManageGroup
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();
        string strQuery = ""; string strQuery1 = ""; string strQuery2 = "";

        #region settings User Group Management

        public DataTable ShowGroupmgmt()
        {
            DataTable dtgroup = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_Groupmgmt();
                dtgroup = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "ShowGroupmgmt Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "ShowGroupmgmtMicroService", "ShowGroupmgmt Exception : " + ex.Message.ToString(), true);
            }
            return dtgroup;
        }
        public DataTable bindgroup_data()
        {
            DataTable dtbindgrp = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_type("4", "1");
                dtbindgrp = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "bindgroup_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "bindgroup_dataMicroService", "bindgroup_data Exception : " + ex.Message.ToString(), true);
            }
            return dtbindgrp;
        }
        public string bindmenu_data(string product_id)
        {
            DataTable dtbindmenu = new DataTable();
            string nodeslist = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.group_rights_data(product_id);
                dtbindmenu = objDB.getDatatable(strQuery.ToString());

                List<TreeNode> nodes = new List<TreeNode>();
                if (dtbindmenu != null && dtbindmenu.Rows.Count > 0)
                {
                    foreach (DataRow row in dtbindmenu.Rows)
                    {
                        nodes.Add(new TreeNode { id = row["menu_id"].ToString(), text = row["menu_name"].ToString(), parent = row["menu_parent_id"].ToString(), active = row["active"].ToString() });
                    }
                    nodeslist = JsonConvert.SerializeObject(nodes);
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "bindmenu_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "bindmenu_dataMicroService", "bindmenu_data Exception : " + ex.Message.ToString(), true);
            }
            return nodeslist;
        }
        public string bindmenu_data()
        {
            DataTable dtbindmenu = new DataTable();
            string nodeslist = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.group_rights_data();
                dtbindmenu = objDB.getDatatable(strQuery.ToString());

                List<TreeNode> nodes = new List<TreeNode>();
                if (dtbindmenu != null && dtbindmenu.Rows.Count > 0)
                {
                    foreach (DataRow row in dtbindmenu.Rows)
                    {
                        nodes.Add(new TreeNode { id = row["menu_id"].ToString(), text = row["menu_name"].ToString(), parent = row["menu_parent_id"].ToString(), active = row["active"].ToString() });
                    }
                    nodeslist = JsonConvert.SerializeObject(nodes);
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "bindmenu_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "bindmenu_dataMicroService", "bindmenu_data Exception : " + ex.Message.ToString(), true);
            }
            return nodeslist;
        }
        public string addgroup(string groupname, string groupdescription, string grouptype)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            string entity_id = "";
            //string group_id = "";
            //string menu_id = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                //if (menuid != null)
                //    menuid = menuid.Substring(1);
                strQuery = objQuery.Get_group_count(groupname, "4");
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (cnt == 0)
                {
                    strQuery1 = objQuery.get_entity_master_id();
                    entity_id = objDB.executeScalar(strQuery1.ToString());

                    strQuery2 = objQuery.addgroup(grouptype, entity_id, groupname, groupdescription, "4");
                    querystatus = objDB.execute(strQuery2.ToString());
                    
                    if (querystatus == 1)
                        result = "Group added successfully.";
                    else
                        result = "Group added failed.";
                }
                else
                    result = "Group already exists.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "addgroup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "addgroupMicroService", "addgroup Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable get_group_data(string groupid)
        {
            DataTable dtgetgrp = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_group_data(groupid);
                dtgetgrp = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "get_group_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "get_group_dataMicroService", "get_group_data Exception : " + ex.Message.ToString(), true);
            }
            return dtgetgrp;
        }
        public string editgroup(string groupname, string groupdescription, string grouptype, string groupid)
        {
            int querystatus = 0;
            string result = "";
            //string group_id = "";
            //string menu_id = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                //if (menuid != null)
                //    menuid = menuid.Substring(1);
                strQuery = objQuery.editgroup(grouptype, groupdescription, groupid, "4");
                querystatus = objDB.execute(strQuery.ToString());
                
                if (querystatus == 1)
                    result = "Group updated successfully.";
                else
                    result = "Group updated failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "editgroup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "editgroupMicroService", "editgroup Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string deletegroupdata(string groupid)
        {
            //int cnt = 0;
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                //strQuery = objQuery.access_rights_master_count_group(groupid);
                //cnt = objDB.getIntValue(strQuery.ToString());
                //if(cnt > 0)
                //{
                //    strQuery1 = objQuery.access_rights_master_update_groupid(groupid);
                //    objDB.execute(strQuery1.ToString());
                //}
                strQuery2 = objQuery.deletegroupdata(groupid, "4");
                querystatus = objDB.execute(strQuery2.ToString());
                if (querystatus == 1)
                    result = "Group deleted successfully.";
                else
                    result = "Group deleted failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageGroup", "deletegroupdata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageGroup", "log", "deletegroupdataMicroService", "deletegroupdata Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion
    }
}
