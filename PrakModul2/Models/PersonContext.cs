using Npgsql;
using PrakModul2.Helpers;

namespace PrakModul2.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;
        public PersonContext(string pConstr) 
        {
            __constr = pConstr;
        }
        public List<Person> ListPerson() 
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_murid, nama_murid, alamat_murid, kelas FROM users.person;");
            sqlDBHelpers db = new sqlDBHelpers(this.__constr);
            try 
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {

                    list1.Add(new Person()
                    { 
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama_murid = reader["nama_murid"].ToString(),
                        alamat_murid = reader["alamat_murid"].ToString(),
                        kelas = reader["kelas"].ToString(),
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            
            }
            catch (Exception ex) 
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        public Person GetPerson(int id)
        {
            Person person = null;
            string query = @"SELECT id_murid, nama_murid, alamat_murid, kelas FROM users.person WHERE id_murid = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                person = new Person
                {
                    id_murid = int.Parse(reader["id_murid"].ToString()),
                    nama_murid = reader["nama_murid"].ToString(),
                    alamat_murid = reader["alamat_nurid"].ToString(),                  
                    kelas = reader["kelas"].ToString(),
                };
            }

            reader.Close();
            cmd.Dispose();
            conn.Close();

            return person;
        }

        public void AddPerson(Person person)
        {
            string query = @"INSERT INTO users.person (nama_murid, alamat_murid, kelas) VALUES (@nama_murid, @alamat_murid, @kelas)";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nama_murid", person.nama_murid);
            cmd.Parameters.AddWithValue("@alamat_murid", person.alamat_murid);
            cmd.Parameters.AddWithValue("@kelas", person.kelas);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void UpdatePerson(Person person, int iduser)
        {
            string query = @"UPDATE users.person SET nama_murid = @nama_murid, alamat_murid = @alamat_murid, kelas = @kelas WHERE id_murid = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", iduser);
            cmd.Parameters.AddWithValue("@nama_murid", person.nama_murid);
            cmd.Parameters.AddWithValue("@alamat_murid", person.alamat_murid);
            cmd.Parameters.AddWithValue("@kelas", person.kelas);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void DeletePerson(int id)
        {
            string query = @"DELETE FROM users.person WHERE id_murid = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

    }
}
