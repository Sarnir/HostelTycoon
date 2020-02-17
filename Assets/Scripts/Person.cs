using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex
{
    Male,
    Female
}

public class Person
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

    public Person()
    {
        Name = names[Random.Range(0, names.Length)];
        Sex = Random.value > 0.5f ? Sex.Male : Sex.Female;
        Avatar = Resources.Load<Sprite>("Avatars/avatar" + Random.Range(1, 6));
    }
}