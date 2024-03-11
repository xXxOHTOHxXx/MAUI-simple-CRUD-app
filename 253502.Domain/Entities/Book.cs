using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _253502.Domain.Entities
{
    /// <summary>
    /// Слушатель курсов
    /// </summary>
    public class Book : Entity
    {
        private Book()
        {
        }
        public Book(BookInfo personalData, double? rate = 0)
        {
            InfoData = personalData;
            Rating = rate.Value;
        }
        // Имя и дата рождения описаны в отдельном классе
        public BookInfo InfoData { get; set; }
        public double Rating { get; set; }
        // Идентификатор курса
        public int? AuthorID { get; set; }
        /// <summary>
        /// Зачислить на курс
        /// </summary>
        /// <param name="courceId">Идентификатор курса</param>
        public void AddToAuthor(int authorid)
        {
            if (authorid <= 0) return;
            AuthorID = authorid;
        }
        /// <summary>
        /// Отчислить с курса
        /// </summary>
        public void RemoveFromAuthor()
        {
            AuthorID = 0;
        }
        /// <summary>
        /// Изменить средний балл
        /// </summary>
        /// <param name="rate"></param>
        public void ChangeRating(double rate)
        {
            if (rate < 0 || rate > 10) return;
            Rating = rate;
        }
    }
}
