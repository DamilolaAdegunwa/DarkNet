using Dark.Core.Application.Service.Dto;
using Dark.Core.Domain.Entity;
using Dark.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Repository
{
    public interface IDbHelper
    {
       
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        int Insert<T>(T entity) where T :EntityBase;

        void InsertList<T>(List<T> insertList, string tableName = "") where T : EntityBase;
      
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Update<T>(T entity, Dictionary<string, object> changeCols) where T : EntityBase;

        
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        void Delete<T>(int? Id) where T : EntityBase;


        /// <summary>
        /// 通过id来获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetById<T>(int id) where T : EntityBase;
        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>() where T : EntityBase;
        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetByPage<T>(PageDto page) where T : EntityBase;

        /// <summary>
        /// 执行普通的SQL
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string strSQL,params SqlParameter[] parameters);

        /// <summary>
        /// 在事物中执行SQL
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        void ExecuteNonQueryWithTrans(string strSQL);
    }
}
