/*
    Author:
    Anne Sophie Wiuff, anwi2107@student.miun.se

*/
using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

namespace guestbook
{
    public class GuestBook
    {
        private string filename = @"guestbook.json";
        private List<GuestbookEntry> entries = new List<GuestbookEntry>();

        public GuestBook()
        {
            if (File.Exists(@"guestbook.json") == true)
            { // If json data exist, the program will run it 
                string jsonString = File.ReadAllText(filename);
                entries = JsonSerializer.Deserialize<List<GuestbookEntry>>(jsonString);
            }
        }



        //method for adding a new message to the guestbook
        public GuestbookEntry addMsg(GuestbookEntry msg)
        {
            entries.Add(msg);
            marshal();
            return msg;
        }

        
        //method for deleting a message
        public int delMsg(int index)
        {
            entries.RemoveAt(index);
            marshal();
            return index;
        }

        public List<GuestbookEntry> getMsgs()
        {
            return entries;
        }

        private void marshal()
        {
            var jsonString = JsonSerializer.Serialize(entries);
            File.WriteAllText(filename, jsonString);
        }
    }

    //setting & getting entries & author in guestbook
    public class GuestbookEntry
    {

        //set & get for content
        private string entry_no;
        public string Entry_no
        {
            set { this.entry_no = value; }
            get { return this.entry_no; }
        }


        //set & get for author
        private string author_name;
        public string Author_name
        {
            set { this.author_name = value; }
            get { return this.author_name;  }
        }


    }

    //class for the program
    class Program
    {
        static void Main(string[] args)
        {

            GuestBook guestbook = new GuestBook();
            int i = 0;

            while (true)
            {
                Console.Clear(); Console.CursorVisible = false;
                Console.WriteLine("Guestbook of Wiuff family\n\n");

                Console.WriteLine("1. Add entry");
                Console.WriteLine("2. Delete entry\n");
                Console.WriteLine("X. Exit\n");

                i = 0;

                //lopping through the entries 
                foreach (GuestbookEntry msgs in guestbook.getMsgs())
                {
                    Console.WriteLine("[" + i++ + "] " + "Name: " + msgs.Author_name + "\n" + "Message: " + msgs.Entry_no);
                }

                int inp = (int)Console.ReadKey(true).Key;

                //depending on what key is pressed, switch method
                switch (inp)
                {
                    case '1':
                        Console.CursorVisible = true;
                        GuestbookEntry obj = new GuestbookEntry();
                        Console.WriteLine("Write your name");
                        string author = Console.ReadLine();
                        obj.Author_name = author;
                        if (String.IsNullOrEmpty(author))
                        {
                            Console.WriteLine("Name must be written!");
                            Console.ReadKey(true);
                        }else
                        {
                            Console.Write("Write your message: ");
                            string message = Console.ReadLine();
                            obj.Entry_no = message;
                            if (String.IsNullOrEmpty(message))
                                {
                                Console.WriteLine("Entry in guestbook must not be empty! Press random key and start over");
                                Console.ReadKey(true);
                                }else
                            {
                                
                                guestbook.addMsg(obj);
                               
                            }
                        }
                        break;

                    case '2':
                        Console.CursorVisible = true;
                        Console.Write("Index to delete: ");
                        string index = Console.ReadLine();
                        guestbook.delMsg(Convert.ToInt32(index));
                        break;
                    case 88:
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}


