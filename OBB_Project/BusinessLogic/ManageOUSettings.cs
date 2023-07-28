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
    public class ManageOUSettings
    {
        QueryHandler objQuery = new QueryHandler();
        DatabaseCommon objcommon = new DatabaseCommon();
        DataTableConversion dtconversion = new DataTableConversion();

        string strQuery = ""; string strQuery1 = ""; string strQuery2 = ""; string strQuery3 = ""; string strQuery4 = ""; string strQuery5 = ""; string strQuery6 = ""; string strQuery7 = ""; string strQuery8 = ""; string strQuery9 = ""; string strQuery10 = ""; string strQuery11 = ""; string strQuery12 = ""; string strQuery13 = "";

        #region User Type

        public DataTable Get_usertype_datatable()
        {
            DataTable dtusertype = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_usertype_datatable("1");
                dtusertype = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOUSettings", "Get_usertype_datatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Get_usertype_datatableMicroService", "Get_usertype_datatable Exception : " + ex.Message.ToString(), true);
            }
            return dtusertype;
        }
        public DataTable Get_product_type()
        {
            DataTable dtproduct = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_product_type();
                dtproduct = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageOUSettings", "Get_product_type Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Get_product_typeMicroService", "Get_product_type Exception : " + ex.Message.ToString(), true);
            }
            return dtproduct;
        }
        public string save_usertype_data(string userjson)
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
                int id1 = Int16.Parse(display["id"]);
                strQuery = objQuery.entity_defination_master_count("1", display["user_type"], display["product"]);
                cnt = Convert.ToInt16(objDB.executeScalar(strQuery));
                if (id1 == 1)
                {
                    if (cnt == 0)
                    {
                        strQuery1 = objQuery.get_entity_instance_id_usertype("1", display["product"]);
                        instance_id = objDB.executeScalar(strQuery1.ToString());

                        strQuery2 = objQuery.insert_entity_defination_master("1", instance_id, display["user_type"], display["product"]);
                        querystatus = objDB.execute(strQuery2.ToString());

                        //StartUp Screen save
                        strQuery3 = objQuery.access_rights_template_insert("1", instance_id, display["startup"], "1", display["product"]);
                        objDB.execute(strQuery3.ToString());

                        if (querystatus >= 1)
                            result = "User type added successfully.";
                        else
                            result = "User type added failed.";
                    }
                    else
                    {
                        result = "User type already exists.";
                    }
                }
                else if (id1 == 2)
                {
                    //strQuery1 = objQuery.get_entity_instance_id_serialno(serialno, "1");
                    strQuery1 = objQuery.get_entity_instance_id_name_usertype("1", display["user_type"], display["product"]);
                    instance_id = objDB.executeScalar(strQuery1.ToString());

                    strQuery2 = objQuery.access_rights_template_update(display["startup"], instance_id, display["product"]);
                    querystatus = objDB.execute(strQuery2.ToString());

                    if (querystatus >= 1)
                        result = "User type updated successfully.";
                    else
                        result = "User type updated failed.";
                }
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "save_usertype_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "save_usertype_dataMicroService", "save_usertype_data Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable get_editdata(string userjson)
        {
            DataTable dtgetedit = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Get_edit_usertypedata(display["instance_name"], "1", display["product"]);
                dtgetedit = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //objcommon.WriteLog("ManageOUSettings", "get_editdata Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "get_editdataMicroService", "get_editdata Exception : " + ex.Message.ToString(), true);
            }
            return dtgetedit;
        }
        public string Delete_usertype(string userjson)
        {
            int cnt = 0; int cnt1 = 0; int cnt2 = 0; int cnt3 = 0;
            int querystatus = 0;
            string result = "";
            string entity_type = "";
            string entity_instance = "";
            DataTable dtinstanceid = new DataTable();
            DataTable dtuserids = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.entity_defination_master_entity_instancename(display["instance_name"], display["product"]);
                dtinstanceid = objDB.getDatatable(strQuery.ToString());
                entity_type = dtinstanceid.Rows[0]["entity_type_id"].ToString();
                entity_instance = dtinstanceid.Rows[0]["entity_instance_id"].ToString();

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
                    strQuery4 = objQuery.entity_master_userids(entity_type, entity_instance);
                    dtuserids = objDB.getDatatable(strQuery4.ToString());
                    for (int i = 0; i < dtuserids.Rows.Count; i++)
                    {
                        int cnt4 = 0;
                        strQuery5 = objQuery.access_rights_master_count_user(dtuserids.Rows[i]["entity_id"].ToString());
                        cnt4 = Convert.ToInt16(objDB.executeScalar(strQuery5));
                        if (cnt4 > 0)
                        {
                            strQuery6 = objQuery.delete_user_rights_data(dtuserids.Rows[i]["entity_id"].ToString());
                            objDB.execute(strQuery6.ToString());
                        }
                    }

                    strQuery7 = objQuery.entity_master_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery7.ToString());
                }

                strQuery8 = objQuery.get_entity_adhoc_info_count(entity_type, entity_instance);
                cnt2 = Convert.ToInt16(objDB.executeScalar(strQuery8));
                if (cnt2 > 0)
                {
                    strQuery9 = objQuery.entity_adhoc_info_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery9.ToString());
                }

                strQuery10 = objQuery.Get_ou_entity_relation_count(entity_type, entity_instance);
                cnt3 = Convert.ToInt16(objDB.executeScalar(strQuery10));
                if (cnt3 > 0)
                {
                    strQuery11 = objQuery.ou_entity_relation_update_delete(entity_type, entity_instance);
                    objDB.execute(strQuery11.ToString());
                }

                strQuery12 = objQuery.access_rights_template_deleteall(entity_type, entity_instance, display["product"]);
                objDB.execute(strQuery12.ToString());

                strQuery13 = objQuery.delete_usertype_entity_defination_master(display["instance_name"], display["product"]);
                querystatus = objDB.execute(strQuery13.ToString());

                if (querystatus == 1)
                    result = "User type deleted successfully.";
                else
                    result = "User type deleted failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //    objcommon.WriteLog("ManageOUSettings", "Delete_usertype Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Delete_usertypeMicroService", "Delete_usertype Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

        #region Link User

        public DataTable Get_linkuser_datatable()
        {
            DataTable dtlinkuser = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_linkuser_datatable();
                dtlinkuser = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "Get_linkuser_datatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Get_linkuser_datatableMicroService", "Get_linkuser_datatable Exception : " + ex.Message.ToString(), true);
            }
            return dtlinkuser;
        }
        public DataTable linkgroup(string userid)
        {
            DataTable dtlinkgrp = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_linkgroup(userid);
                dtlinkgrp = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("ManageOUSettings", "linkgroup Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "linkgroupMicroService", "linkgroup Exception : " + ex.Message.ToString(), true);
            }
            return dtlinkgrp;
        }
        public string get_user_name(string userid)
        {
            string result = "";
            string strQuery = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_user_name(userid);
                result = objDB.executeScalar(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "get_user_name Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "get_user_nameMicroService", "get_user_name Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string apply_link_group(string userjson)
        {
            int cnt = 0; int cnt1 = 0;
            int querystatus = 0;
            string result = "";
            DataTable dtapplygrp = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                if (display["group_ids"] != null)
                {
                    string group_ids = display["group_ids"].Substring(1);
                    string[] groupid = group_ids.Split(',');
                    for (int i = 0; i < groupid.Length; i++)
                    {
                        strQuery = objQuery.Get_menuid_linkuser(groupid[i].ToString());
                        dtapplygrp = objDB.getDatatable(strQuery.ToString());
                        if (dtapplygrp.Rows.Count > 0)
                        {
                            strQuery1 = objQuery.check_user_group_id_linkuser(display["user_id"], groupid[i].ToString());
                            cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                            if (cnt == 0)
                            {
                                strQuery2 = objQuery.check_user_id_linkuser(display["user_id"]);
                                cnt1 = Convert.ToInt16(objDB.executeScalar(strQuery2));
                                if (cnt1 > 0)
                                {
                                    strQuery3 = objQuery.delete_userid_linkuser(display["user_id"]);
                                    Convert.ToInt16(objDB.executeScalar(strQuery3));
                                }
                                for (int j = 0; j < dtapplygrp.Rows.Count; j++)
                                {
                                    strQuery4 = objQuery.access_rights_master_insert(display["user_id"], groupid[i].ToString(), dtapplygrp.Rows[j]["menu_id"].ToString());
                                    querystatus = objDB.execute(strQuery4.ToString());
                                }
                            }
                            else
                            {
                                strQuery5 = objQuery.delete_user_rights_data_linkuser(display["user_id"], groupid[i].ToString());
                                Convert.ToInt16(objDB.executeScalar(strQuery5));
                            }
                        }
                        else
                        {
                            strQuery6 = objQuery.access_rights_master_insert(display["user_id"], groupid[i].ToString(), "0");
                            querystatus = Convert.ToInt16(objDB.executeScalar(strQuery6));
                        }
                    }
                    if (querystatus == 1)
                        result = "Group link apply successfully.";
                    else
                        result = "Group link apply failed.";
                }
                else
                    result = "Select any one group name ! ";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "apply_link_group Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "apply_link_groupMicroService", "apply_link_group Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable GetOUChecked(string user_id)
        {
            DataTable dtoucheck = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_ou_data();
                dtoucheck = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "GetOUChecked Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "GetOUCheckedMicroService", "GetOUChecked Exception : " + ex.Message.ToString(), true);
            }
            return dtoucheck;
        }
        public DataTable GetOUdatatable(string userid)
        {
            DataTable dtou = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_ou_entity_relation(userid);
                dtou = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("ManageOUSettings", "GetOUdatatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "GetOUdatatableMicroService", "GetOUdatatable Exception : " + ex.Message.ToString(), true);
            }
            return dtou;
        }
        public string link_user_with_ou(string userjson)
        {
            int cnt = 0;
            int querystatus = 0;
            string result = "";
            DataTable dtuserlink = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                string user_ids = display["user_ids"].Substring(1);
                strQuery = objQuery.entity_master_userlinkup_data(display["user_ids"]);
                dtuserlink = objDB.getDatatable(strQuery.ToString());
                foreach (DataRow dr in dtuserlink.Rows)
                {
                    strQuery1 = objQuery.check_user_exist_ou_linkup(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), dr["entity_id"].ToString());
                    cnt = Convert.ToInt16(objDB.executeScalar(strQuery1));
                    if (cnt > 0)
                    {
                        strQuery2 = objQuery.ou_entity_relation_update_delete_oulinkup(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), dr["entity_id"].ToString());
                        objDB.execute(strQuery2.ToString());
                    }

                    strQuery3 = objQuery.insert_update_user_ou_linkup(dr["entity_type_id"].ToString(), dr["entity_instance_id"].ToString(), display["ou_id"], dr["entity_id"].ToString(), display["child"]);
                    querystatus = objDB.execute(strQuery3.ToString());
                }

                if (querystatus == 1)
                    result = "OU linked with user successfully.";
                else
                    result = "OU linked with user failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("MangeOUSettings", "link_user_with_ou Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "link_user_with_ouMicroService", "link_user_with_ou Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public string Delete_link_ou(string userjson)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.Delete_link_ou(display["ou_nodelinkage_ouid"], display["userid"]);
                querystatus = objDB.execute(strQuery.ToString());
                if (querystatus == 1)
                    result = "Unlink OU successfully.";
                else
                    result = "Unlink OU failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("MangeOUSettings", "Delete_link_ou Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Delete_link_ouMicroService", "Delete_link_ou Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

        #region Connect Parent

        public DataTable get_parent_data()
        {
            DataTable dtparent = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.get_parent_data();
                dtparent = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("MangeOUSettings", "get_parent_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "get_parent_dataMicroService", "get_parent_data Exception : " + ex.Message.ToString(), true);
            }
            return dtparent;
        }
        public string save_parent_data(string userjson)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.update_location_installation_linkage(display["ou_id"], display["location_id"], display["ip"]);
                querystatus =objDB.execute(strQuery.ToString());
                if (querystatus == 1)
                    result = "Parent data added successfully.";
                else
                    result = "Parent data added failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("MangeOUSettings", "save_parent_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "save_parent_dataMicroService", "save_parent_data Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion

        #region Child Installation

        public string save_child_data(string userjson)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.insert_location_installation_linkage(display["ou_id"], display["location_id"], display["ip"], display["location_prefix"]);
                querystatus = objDB.execute(strQuery.ToString());
                if (querystatus == 1)
                    result = "Child installation data added successfully.";
                else
                    result = "Child installation data added failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("MangeOUSettings", "save_child_data Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "save_child_dataMicroService", "save_child_data Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }
        public DataTable Get_child_datatable()
        {
            DataTable dtchild = new DataTable();
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                strQuery = objQuery.Get_child_datatable();
                dtchild = objDB.getDatatable(strQuery.ToString());
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                //  objcommon.WriteLog("MangeOUSettings", "Get_child_datatable Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "Get_child_datatableMicroService", "Get_child_datatable Exception : " + ex.Message.ToString(), true);
            }
            return dtchild;
        }
        public string Deletedetail(string userjson)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                DatabaseHandler objDB = LocalConstant.poolOBBPool.getConnection();
                string strTemp = dtconversion.Base64Decode(userjson);
                IDictionary<string, string> display = dtconversion.getJSONPropertiesFromString("[" + strTemp + "]");
                strQuery = objQuery.delete_location_installation_linkage(display["ouid"], display["location_id"], display["ip"], display["location_prefix"]);
                querystatus = objDB.execute(strQuery.ToString());
                if (querystatus == 1)
                    result = "Child data deleted successfully.";
                else
                    result = "Child data deleted failed.";
                LocalConstant.poolOBBPool.returnConnection(objDB);
            }
            catch (Exception ex)
            {
                // objcommon.WriteLog("MangeOUSettings", "Deletedetail Exception : " + ex.Message.ToString());
                objcommon.WriteLog("ManageOUSettings", "log", "DeletedetailMicroService", "Deletedetail Exception : " + ex.Message.ToString(), true);
            }
            return result;
        }

        #endregion
    }
}
