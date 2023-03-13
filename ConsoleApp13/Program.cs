using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp13.Program;

namespace ConsoleApp13
{
    internal class Program
    {
        #region Состав : 
        public enum Tip { Professor, Teacher, Director }
        public enum Pol { Man, Woman }
        public class Universitet
        {
            public List<Faculteti> Faculties { get; set; }
            public Universitet() { }
            public Universitet(List<Faculteti> faculties) => Faculties = faculties;
        }
        public class Faculteti
        {
            public string Name_facultet { get; set; }
            public List<Sostav> Sostav { get; set; }
            public Faculteti() { }
            public Faculteti(string name_facultet, List<Sostav> sostav)
            {
                Name_facultet = name_facultet;
                Sostav = sostav;
            }

        }
        public class Sostav
        {
            public string Name_Sostav { get; set; }
            public Teachers Head_Teacher { get; set; }
            public bool Predmets { get; set; }
            public List<Class_room> Classroom { get; set; }
            public List<Teachers> Teacher { get; set; }
            public Sostav()
            {
                Name_Sostav = null;
                Head_Teacher = null;
                Predmets = false;
                Classroom = null;
                Teacher = null;
            }
            public Sostav(string name_Sostav, Teachers head, bool predmet, List<Class_room> classroom, List<Teachers> teachers)
            {
                Name_Sostav = name_Sostav;
                Head_Teacher = head;
                Predmets = predmet;
                Classroom = classroom;
                Teacher = teachers;
            }
        }
        #endregion
        public class People
        {
            public string Imya { get; set; }
            public string Familiya { get; set; }
            public Pol Gender { get; set; }
            public string Pasport { get; set; }
            public string Adress { get; set; }
            public People()
            {
                Imya = null;
                Familiya = null;
                Pasport = null;
                Adress = null;
            }
            public People(string imya, string familiya, Pol gender, string pasport, string adress)
            {
                Imya = imya;
                Familiya = familiya;
                Gender = gender;
                Pasport = pasport;
                Adress = adress;
            }
            public override string ToString()
            {
                return $"Имя{Imya} {Familiya}\nГендер{Gender}";
            }

        }
        public class Students : People
        {
            public List<Roditeli> Roditeli { get; set; }
            public Class_room Classroom { get; set; }
            public Students() : base()
            {
                Roditeli = null;
                Classroom = null;
            }
            public Students(string imya, string familiya, Pol pol, string pasport, string adress, Class_room classroom, List<Roditeli> parents) : base(imya, familiya, pol, pasport, adress)
            {
                Classroom = classroom;
                Roditeli = parents;
            }
            public Students(string imya, string familiya, Pol pol, string pasport, string adress, Class_room classroom) : base(imya, familiya, pol, pasport, adress)
            {
                Classroom = classroom;
                Roditeli = null;
            }
            public override string ToString()
            {
                return base.ToString() + $"\nКлассный состав{Classroom.Name_class}";
            }
        }
        public class Teachers : People
        {
            public Sostav Sostav { get; set; }
            public Tip Tip { get; set; }

            public Teachers() : base() => Sostav = null;
            public Teachers(string imya, string familiya, Pol pol, string pasport, string adress, Sostav department, Tip tip) : base(imya, familiya, pol, pasport, adress)
            {
                Sostav = department;
                Tip = tip;
            }
            public override string ToString()
            {
                return base.ToString() + $"\nФакультет{Sostav.Name_Sostav}\nГлова{Tip}";
            }
        }
        public class Class_room
        {
            public string Name_class { get; set; }
            public Students Head_class { get; set; }
            public Sostav Profil { get; set; }
            public Class_room()
            {
                Name_class = null;
                Head_class = null;
                Profil = null;
            }
            public Class_room(string name_class, Sostav profil, Students head_class)
            {
                Name_class = name_class;
                Profil = profil;
                Head_class = head_class;
            }
        }
        public class Roditeli : People
        {
            public List<Students> Deti { get; set; }
            public Roditeli() : base() => Deti = null;
            public Roditeli(string imya, string familiya, Pol pol, string pasport, string adress, List<Students> deti) : base(imya, familiya, pol, pasport, adress)
            {
                Deti = deti;
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        #region Список 
        public class Katalog
        {
            public List<Students> Students { get; set; }
            public List<Teachers> Teachers { get; set; }
            public List<Roditeli> Parents { get; set; }
            public List<Faculteti> Facultetis { get; set; }
            public List<Students> Sostav_deti { get; set; }

            public Katalog()
            {
                Students = new List<Students>();
                Teachers = new List<Teachers>();
                Parents = new List<Roditeli>();
                Facultetis = new List<Faculteti>();
                Sostav_deti = new List<Students>();
            }
            #endregion
            public List<Students> GetSortSostav(string sortBy)
            {
                switch (sortBy)
                {
                    case "Imya":
                        return Students.OrderBy(a => a.Imya).ThenBy(a => a.Familiya).ToList();
                    case "Facultetis":
                        return Students.OrderBy(a => a.Classroom.Profil).ThenBy(a => a.Classroom.Name_class).ToList();
                    case "Class_room":
                        return Students.OrderBy(a => a.Classroom.Name_class).ThenBy(a =>  a.Familiya).ThenBy(a => a.Imya).ToList();
                    default:
                        return Students.OrderBy(a => a.Familiya).ThenBy(a => a.Imya).ToList();
                }
            }
            public List<Teachers> GetSortStaff(string sortBy)
            {
                switch (sortBy)
                {
                    case "Imya":
                        return Teachers.OrderBy(b => b.Familiya).ThenBy(b => b.Imya).ToList();
                    case "Facultetis":
                        return Teachers.OrderBy(b => b.Sostav.Predmets).ThenBy(b => b.Sostav.Name_Sostav).ThenBy(b => b.Familiya ).ThenBy(b => b.Imya).ToList();
                    case "Class_room":
                        return Teachers.OrderBy(b => b.Sostav.Name_Sostav).ThenBy(b => b.Familiya).ThenBy(b => b.Imya).ToList();
                    default:
                        return Teachers.OrderBy(b => b.Familiya).ThenBy(b => b.Imya).ToList();
                }
            }

            public List<Teachers> GetSortHead()
            {
                return Teachers.Where(b => b.Sostav.Predmets).ToList();
            }
            public List<Students> Get_deti_with_Roditeli(Roditeli roditeli)
            {
                return Students.Where(a => a.Roditeli.Contains(roditeli)).ToList();
            }
            public List<Students> Get_Sort_Staff_with_Deti()
            {
                return Sostav_deti.OrderBy(a => a).ThenBy(a => a.Familiya).ToList();
            }
        }

        static void Main(string[] args)
        {

        }
    }
}
