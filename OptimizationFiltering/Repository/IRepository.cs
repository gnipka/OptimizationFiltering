using OptimizationFiltering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Repository
{
    internal interface IRepository<T> where T : class
    {
        /// <summary>
        /// Добавление элемента в бд
        /// </summary>
        /// <param name="item">Экземпляр класса</param>
        public void Create(T item);

        /// <summary>
        /// Просмотр элемента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Read(int id);


        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item);

        /// <summary>
        /// Удаление по экземпляру класса
        /// </summary>
        /// <param name="item"></param>
        public void Delete(T item);

        /// <summary>
        /// Удаление по id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
    }
}
