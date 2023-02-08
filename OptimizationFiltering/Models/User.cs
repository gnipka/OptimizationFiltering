namespace OptimizationFiltering.Models
{
    internal class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя в зашифрованном виде
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ссылка на роль
        /// </summary>
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
