using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _253502.Domain.Entities
{

    public class Author : Entity
    {

        private List<Book> _books = new();
        private Author(){ }
        public Author(string name, DateTime DateOfBirth, int FavoriteNumber)
        {
            Name = name;
            this.DateOfBirth = DateOfBirth;
            this.FavoriteNumber = FavoriteNumber;
        }
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int FavoriteNumber { get; set; }

        public IReadOnlyList<Book> Books
        {
            get => _books.AsReadOnly();
        }

    }
}

