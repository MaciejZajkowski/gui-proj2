using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models;

    public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int book_id {get;set;}
    public string author {get;set;}
    
    public string user {get;set;}
    public string title {get;set;}
    public string publisher {get;set;}
    public int year {get;set;}
    public bool status {get;set;}

}


