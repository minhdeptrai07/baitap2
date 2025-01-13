using System;
using System.Collections.Generic;
namespace baitap2
{
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Available { get; set; }

        public LibraryItem(string title, string author, DateTime publishDate)
        {
            Title = title;
            Author = author;
            PublishDate = publishDate;
            Available = true;
        }

        public abstract void Checkout();
        public abstract void ReturnItem();

        public void DisplayInfo()
        {
            Console.WriteLine($"{Title} - Tác giả: {Author} - Ngày xuất bản: {PublishDate.ToShortDateString()} - Trạng thái: {(Available ? "Có sẵn" : "Không có sẵn")}");
        }
    }
    public class Book : LibraryItem
    {
        public string Genre { get; set; }

        public Book(string title, string author, DateTime publishDate, string genre)
            : base(title, author, publishDate)
        {
            Genre = genre;
        }

        public override void Checkout()
        {
            if (Available)
            {
                Available = false;
                Console.WriteLine($"Đã mượn sách: {Title}");
            }
            else
            {
                Console.WriteLine($"Sách '{Title}' không có sẵn để mượn.");
            }
        }

        public override void ReturnItem()
        {
            Available = true;
            Console.WriteLine($"Đã trả sách: {Title}");
        }
    }

    public class DVD : LibraryItem
    {
        public int Runtime { get; set; }

        public DVD(string title, string author, DateTime publishDate, int runtime)
            : base(title, author, publishDate)
        {
            Runtime = runtime;
        }

        public override void Checkout()
        {
            if (Available)
            {
                Available = false;
                Console.WriteLine($"Đã mượn DVD: {Title}");
            }
            else
            {
                Console.WriteLine($"DVD '{Title}' không có sẵn để mượn.");
            }
        }

        public override void ReturnItem()
        {
            Available = true;
            Console.WriteLine($"Đã trả DVD: {Title}");
        }
    }


    public class LibraryCatalog
    {
        private List<LibraryItem> items;

        public LibraryCatalog()
        {
            items = new List<LibraryItem>();
        }

        public void AddItem(LibraryItem item)
        {
            items.Add(item);
        }

        public List<LibraryItem> FindItem(string title = null, string author = null)
        {
            var results = new List<LibraryItem>();

            foreach (var item in items)
            {
                if ((title != null && item.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) ||
                    (author != null && item.Author.Contains(author, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public void DisplayCatalog()
        {
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            // Tạo các đối tượng sách và DVD
            var book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", new DateTime(1925, 4, 10), "Fiction");
            var book2 = new Book("1984", "George Orwell", new DateTime(1949, 6, 8), "Dystopian");
            var dvd1 = new DVD("The Matrix", "Wachowskis", new DateTime(1999, 3, 31), 136);
            var dvd2 = new DVD("Inception", "Christopher Nolan", new DateTime(2010, 7, 16), 148);

            // Tạo thư viện và thêm các đối tượng vào catalog
            var catalog = new LibraryCatalog();
            catalog.AddItem(book1);
            catalog.AddItem(book2);
            catalog.AddItem(dvd1);
            catalog.AddItem(dvd2);

            // Hiển thị danh mục hiện tại
            Console.WriteLine("Danh mục hiện tại:");
            catalog.DisplayCatalog();
            Console.WriteLine();

            // Tìm kiếm và kiểm tra sách
            Console.WriteLine("Tìm kiếm theo tiêu đề '1984':");
            var searchResults = catalog.FindItem(title: "1984");
            foreach (var item in searchResults)
            {
                Console.WriteLine($"Tìm thấy: {item.Title} - Tác giả: {item.Author}");
            }

            // Mượn và trả lại sách, DVD
            Console.WriteLine("\nMượn và trả lại các mặt hàng:");
            book1.Checkout();  // Mượn sách
            dvd1.Checkout();   // Mượn DVD
            catalog.DisplayCatalog();

            book1.ReturnItem();  // Trả sách
            dvd1.ReturnItem();   // Trả DVD
            catalog.DisplayCatalog();
        }
    }
}