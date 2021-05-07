using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Empresas.Dominio.Models;
using MySql.Data.MySqlClient;

namespace Empresas.Infra.Data.Repositorio
{
    public class EmpresaRepositorio
    {
        private string connectionString;
        public EmpresaRepositorio()
        {
            connectionString = @"Data Source=localhost; user=root; password=; Initial Catalog=empresas";
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Empresa _empresa)

        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"INSERT INTO cadastro_empresas (CNPJ, INSCRICAO_ESTADUAL, NOME_EMPRESARIAL, NOME_FANTASIA, ATIVIDADE_ECONOMICA, TELEFONE_FIXO, WHATSAPP, LOGRADOURO,
                              NUM_LOGRADOURO, COMPLEMENTO, CEP, BAIRRO, MUNICIPIO, UF, STATUS, DATA_ABERTURA, DATA_MODIFICACAO, DATA_ENCERRAMENTO) 
                              VALUES (@CNPJ, @Inscricao_Estadual, @Nome_Empresarial, @Nome_Fantasia, @Atividade_Economica, @Telefone_Fixo, @Whatsapp, @Logradouro,
                              @Num_Logradouro, @Complemento, @CEP, @Bairro, @Municipio, @UF, @Status, @Data_Abertura, @Data_Modificacao, @Data_Encerramento)";
                dbConnection.Open();
                dbConnection.Execute(query, _empresa);
            }

        }

        public IEnumerable<Empresa> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT * FROM cadastro_empresas";
                dbConnection.Open();
                return dbConnection.Query<Empresa>(query);
            }
        }

        public Empresa GetByCNPJ(string cnpj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT * FROM cadastro_empresas WHERE CNPJ=@cnpj";
                dbConnection.Open();
                return dbConnection.Query<Empresa>(query, new { CNPJ = cnpj }).FirstOrDefault();
            }
        }

        public void Delete(string cnpj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"DELETE FROM cadastro_empresas WHERE CNPJ=@cnpj";
                dbConnection.Open();
                dbConnection.Execute(query, new { CNPJ = cnpj });
            }
        }

        public void Update(Empresa _empresa)
        {
            using (IDbConnection dbConnection = Connection)
            {

                string query = @"UPDATE cadastro_empresas SET INSCRICAO_ESTADUAL=@Inscricao_Estadual, NOME_EMPRESARIAL=@Nome_Empresarial, NOME_FANTASIA=@Nome_Fantasia, 
                            ATIVIDADE_ECONOMICA=@Atividade_Economica, TELEFONE_FIXO=@Telefone_Fixo, WHATSAPP=@Whatsapp, LOGRADOURO=@Logradouro, 
                            NUM_LOGRADOURO=@Num_Logradouro, COMPLEMENTO=@Complemento, CEP=@CEP, BAIRRO=@Bairro, MUNICIPIO=@Municipio, UF=@UF, 
                            STATUS=@Status, DATA_ABERTURA=@Data_Abertura, DATA_MODIFICACAO=@Data_Modificacao, DATA_ENCERRAMENTO=@Data_Encerramento where CNPJ=@CNPJ";
                dbConnection.Open();
                dbConnection.Query(query, _empresa);
            }
        }
    }
}
