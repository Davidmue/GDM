using GestionDuMaterielDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMaterielDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("connexion à la base ... ");
            GestionDuMaterielEntities entities = new GestionDuMaterielEntities();
            Console.WriteLine("connexion à la base OK ! ");

            
            //TypeMateriel tm = new TypeMateriel()
            //{
            //    Id = -1,
            //    Description = "Multifonction"
            //};
            //entities.TypeMateriels.Add(tm);
            //entities.SaveChanges();


            //List<TypeMateriel> typesMateriel = new List<TypeMateriel>(entities.TypeMateriels);
            //foreach (TypeMateriel t in typesMateriel)
            //{
            //    if(t.Description.ToString().Trim().ToLower() == "multifonction")
            //    {
            //        Console.WriteLine("Tente de supprimer ... ");
            //        entities.TypeMateriels.Remove(t);
            //    }
            //}
            //entities.SaveChanges();
            List<TypeMateriel> typesMateriel2 = new List<TypeMateriel>(entities.TypeMateriels);
            foreach (TypeMateriel t in typesMateriel2)
            {
                Console.WriteLine("{0} {1}", t.Id.ToString(), t.Description);
            }

            Console.ReadKey(); 

        }
    }
}
