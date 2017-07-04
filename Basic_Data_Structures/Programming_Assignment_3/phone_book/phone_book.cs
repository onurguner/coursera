using System;

namespace phone_book
{
	public class Query {
        public string type;
        public string name;
        public int number;

        public Query(String type, String name, int number) {
            this.type = type;
            this.name = name;
            this.number = number;
        }

        public Query(String type, int number) {
            this.type = type;
            this.number = number;
        }
    }
	
	public class Contact {
        public string name;
        public int number;

        public Contact(String name, int number) {
            this.name = name;
            this.number = number;
        }
    }
	
	public class PhoneBook {
		private string[] contacts = null;
		
		public PhoneBook() {
			contacts = new string[10000000];
		}
		
		public void processQueries() {			
			int queryCount = int.Parse(Console.ReadLine());
			for (int i = 0; i < queryCount; ++i)
				processQuery(readQuery());
		}
		
		private Query readQuery() {
			var tokens = Console.ReadLine().Split(' ');
			string type = tokens[0];
			int number = int.Parse(tokens[1]);
			if (type == "add") {
				string name = tokens[2];
				return new Query(type, name, number);
			} else {
				return new Query(type, number);
			}
		}
		
		private void writeResponse(string response) {
			Console.WriteLine(response);
		}
		
		private void processQuery(Query query) {
			if (query.type == "add") {
				contacts[query.number] = query.name;
			} else if (query.type == "del") {
				contacts[query.number] = "";
			} else {
				string response = "not found";
				if (!string.IsNullOrEmpty(contacts[query.number])) {
					response = contacts[query.number];
				}
				writeResponse(response);
			}
		}
		
	}
	
	class Program {
		static void Main(string[] args)
        {
			new PhoneBook().processQueries();
        }		
	}
}