﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Carbinet
{
    public class EquipmentConfigCtl
    {
        static string sqlInsert_SaveConfig = @"insert into T_EQUIPMENT_LOCATION_MAP(EQUIPEMNTID,IGROUP,IROW,ICOLUMN)
                    values('{0}',{1},{2},{3});";
        static string sqlSelect_CheckConfigExist = @"select EQUIPEMNTID from T_EQUIPMENT_LOCATION_MAP where 
                    IGROUP={0} and IROW = {1} and ICOLUMN = {2}";
        static string sqlSelect_GetAllMapConfigs =
            @"SELECT EQUIPEMNTID,IGROUP,IROW,ICOLUMN from T_EQUIPMENT_LOCATION_MAP;";

        static string sqlUpdate_UpdateConfig = @"update T_EQUIPMENT_LOCATION_MAP set EQUIPEMNTID = '{0}' where 
                    IGROUP={1} and IROW = {2} and ICOLUMN = {3}";

        static string sqlUpdate_UpdateGroupNumber =
            @"update T_CONFIG set VVALUE = '{0}' where VKEY= 'group'";
        static string sqlUpdate_UpdaterowNumber =
             @"update T_CONFIG set VVALUE = '{0}' where VKEY= 'row'";
        static string sqlUpdate_UpdatecolumnNumber =
             @"update T_CONFIG set VVALUE = '{0}' where VKEY= 'column'";

        static string sqlSelect_GetConfigGroupNumber =
            @"select VVALUE from T_CONFIG where VKEY= 'group'";
        static string sqlSelect_GetConfigRowNumber =
            @"select VVALUE from T_CONFIG where VKEY= 'row'";
        static string sqlSelect_GetConfigColumnNumber =
            @"select VVALUE from T_CONFIG where VKEY= 'column'";

        static string sqlDelete_clearMap = "delete from T_EQUIPMENT_LOCATION_MAP;";

        public static bool clearEquipmentMapOfDB()
        {
            try
            {
                int result = (int)CsharpSQLiteHelper.ExecuteNonQuery(sqlDelete_clearMap, null);
                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("更新数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public static DataTable getAllMapConfigs()
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_GetAllMapConfigs, null);
                return dt;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }

        //public int getClassroomConfig(int type)
        //{
        //    string sql = null;
        //    switch (type)
        //    {
        //        case 0:
        //            sql = sqlSelect_GetConfigGroupNumber;
        //            break;
        //        case 1:
        //            sql = sqlSelect_GetConfigRowNumber;
        //            break;
        //        case 2:
        //            sql = sqlSelect_GetConfigColumnNumber;
        //            break;
        //    }
        //    if (null == sql)
        //    {
        //        return 0;
        //    }
        //    try
        //    {
        //        DataTable dt = CsharpSQLiteHelper.ExecuteTable(sql, null);
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            return int.Parse(dr["VVALUE"].ToString());
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("查询数据库时出现错误：" + ex.Message);
        //    }
        //    return 0;
        //}

        /// <summary>
        /// 更新教师的座位配置
        /// </summary>
        /// <param name="number">数目</param>
        /// <param name="type">类型 0 group   1 row  2 column</param>
        /// <returns></returns>
        //public bool updateClassRoomConfig(int number, int type)
        //{
        //    string sql = null;
        //    switch (type)
        //    {
        //        case 0:
        //            sql = sqlUpdate_UpdateGroupNumber;
        //            break;
        //        case 1:
        //            sql = sqlUpdate_UpdaterowNumber;
        //            break;
        //        case 2:
        //            sql = sqlUpdate_UpdatecolumnNumber;
        //            break;
        //    }
        //    if (null == sql)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(sql, new object[1] { number }).ToString());
        //        if (result > 0)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {

        //        MessageBox.Show("更新数据时出现错误：" + ex.Message);
        //    }
        //    return false;
        //}

        public bool updateMapConfig(string equipID, int group, int row, int column)
        {
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(sqlUpdate_UpdateConfig
                             , new object[4] { equipID, group, row, column }).ToString());

                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("更新数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public static void AddMapConfig(List<equipmentPosition> list)
        {
            foreach (equipmentPosition ep in list)
            {
                AddMapConfig(ep);
            }
        }
        public static bool AddMapConfig(equipmentPosition ep)
        {
            string equipID = ep.equipmentID;
            int group = ep.group;
            int row = ep.row;
            int column = ep.column;
            return AddMapConfig(equipID, group, row, column);
        }
        public static bool AddMapConfig(string equipID, int group, int row, int column)
        {
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(sqlInsert_SaveConfig
                              , new object[4] { equipID, group, row, column }).ToString());
                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("添加数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public bool CheckExists(int group, int row, int column)
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(
                       sqlSelect_CheckConfigExist, new object[3] { group, row, column });
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return false;
        }
    }
}
