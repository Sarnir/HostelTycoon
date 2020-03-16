using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex
{
    Male,
    Female
}

public class PersonData
{
    static string[] names =
    {
        "Malik Mckey",
        "Chad Zobel",
        "Daniel Willcutt",
        "Francis Belt",
        "Britt Bouley",
        "Herbert Dibiase",
        "Raphael Zickefoose",
        "Darwin Cail",
        "Antony Gage",
        "Maria Trumble",
        "Ruben Mcquade",
        "Sean Mccracken",
        "Dennis Lenhart",
        "Elwood Lightcap",
        "Sam Lovins",
        "Deshawn Vergara",
        "Timothy Testerman",
        "Collin Verrett",
        "Benito Pompey",
        "Benjamin Voris",
        "Beau Fahey",
        "Lester Macdowell",
        "Vaughn Messer",
        "Wyatt Harville",
        "Boyd Graham",
        "Deandre Weikel",
        "Sammy Wiley",
        "Marco Eickhoff",
        "Isiah Rodas",
        "Kyong Trainer",
        "Kyla Twitty",
        "Jammie Orndorff",
        "Tabitha Pounders",
        "Daine Willison",
        "Lorita Barbeau",
        "Meredith Carabajal",
        "Salley Rathke",
        "Kathey Cort",
        "Mellissa Low",
        "Ena Wagoner",
        "Maragaret Reinert",
        "Onita Sturtevant",
        "Danille Widener",
        "Evette Riter",
        "Hermila Turberville",
        "Julieann Moreles",
        "Roxann Mcginley",
        "Maile Rye",
        "Cherly Haworth"
    };

    public string Name;
    public Sex Sex;
    public Sprite Avatar;
    public int DesiredWage;

    // characteristics:
    // Neatness - brudas czy ocd? <- brudasy lepiej znoszą trudne warunki, ale więcej syfią. pedanci lubią porządek
    // Vigor - leń czy pełny energii? <- lenie wolą pozamulać, ale nie mają wielkich wymagań. hiperaktywni ciągle muszą coś robić bo się nudzą
    // Charisma - nudziarz czy przywódca? <- ???
    // Temperament - anioł czy diaboł? <- ???
    // Character - zły czy dobry? <- ???

    public PersonData()
    {
        Name = names[Random.Range(0, names.Length)];
        Sex = Random.value > 0.5f ? Sex.Male : Sex.Female;
        Avatar = Resources.Load<Sprite>("Avatars/avatar" + Random.Range(1, 6));
        DesiredWage = Random.Range(0, 100);
    }

    // docelowo zmienić employee i guesta na role jako komponenty ludzi
    /*public Guest SpawnGuest(Hostel hostel)
    {
        return Guest.Spawn(this, hostel) as Guest;
    }*/

    public Employee SpawnEmployee(Hostel hostel)
    {
        return Employee.Spawn(this, hostel) as Employee;
    }
}