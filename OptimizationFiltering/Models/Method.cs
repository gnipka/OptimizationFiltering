namespace OptimizationFiltering.Models
{
    internal class Method
    {
        /// <summary>
        /// Идентификатор метода
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование метода
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// true - метод рализован, false - метод не реализован
        /// </summary>
        public bool IsImplemented { get; set; }
    }
}
