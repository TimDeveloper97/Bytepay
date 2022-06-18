using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Helpers
{
    public class RandomHelper
    {
        static string[] surnames = { "Dinh", "Nguyen", "Bao", "Vo", "Vu", "Nhat", "Tran", "Le", "Thai", "Pham",
                                "Phan", "Đang", "Quoc", "Do", "Ho", "Ngo", "Duong", "Ly", "Truong", "Bui", "Duong",
                                "Loc"};

        static string[] cushions = { "Thi", "Duy", "Van", "Thai", "Tat", "Đuc", "Hong", "Viet" };
        static string[] names = { "Anh", "Thuy", "Binh", "Bao", "Van", "Quyen", "Duc", "Vuong", "Tuan", "Minh",
                                "Thai", "Hieu", "Duy", "Tung", "Lieu", "Bao", "Hoang", "Bau", "Dung", "Hai Anh",
                                "Son", "Nguyen", "Thinh", "Manh", "Dinh", "Dung", "Hoa", "Huan", "Tuyen", "Quang",
                                "Tuyet", "Ngoc", "Linh", "Mo", "Vui"};

        static string[] districts = { "Ngô Quyền", "Nam Từ Liêm", "Long Biên", "Liên Chiểu", "Cầu giấy", "Bắc Từ Liêm",
                                "Lê Chân", "Phú Nhuận", "Tân Phú", "Tây Hồ", "Thanh Khê", "Thanh Xuân"};

        static string[] guilds = { "Phúc Thọ", "Đan Phượng", "Hoài Đức", "Quốc Oai", "Hoàng Mai", "Tương Mai",
                                 "Hai Bà Trưng", "Lê Thanh Nghị", "Nguyễn Công trí", "Trần Duy Hưng", 
                                 "Thạch Thất", "Chương Mỹ", "Thanh Oai", "Thường Tín", "Phú Xuyên", "Ứng Hòa"};

        static Random rnd = new Random();

        public static string GetName()
        {
            return surnames[rnd.Next(0, surnames.Length - 1)] + " "
                + cushions[rnd.Next(0, cushions.Length - 1)] + " "
                + names[rnd.Next(0, names.Length - 1)];
        }

        public static string GetPhone() => "0" + rnd.Next(700000000, 999999999).ToString();

        public static string GetEmail(string name = null)
        {
            if(name == null)
            {
                String SALTCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder salt = new StringBuilder();

                while (salt.Length < 10)
                {
                    salt.Append(SALTCHARS[rnd.Next(0, SALTCHARS.Length - 1)]);
                }

                return salt.ToString().ToLower() + "@gmail.com";
            }
            else
            {
                name = name.Replace(" ", "").ToLower();
                var utf8bytes = Encoding.Default.GetBytes(name);
                var email = System.Text.Encoding.Default.GetString(utf8bytes);
                return email + rnd.Next(100, 999).ToString() + "@gmail.com";
            }    
        }

        public static string GetAddress()
        {
            return "Số nhà " + rnd.Next(1, 300).ToString() + ", Ngõ " + rnd.Next(1, 100).ToString() + ", "
                + guilds[rnd.Next(0, guilds.Length - 1)] + ", Tỉnh "
                + districts[rnd.Next(0, districts.Length - 1)] + ", Thành Phố Hà Nội";
        }
    }
}
