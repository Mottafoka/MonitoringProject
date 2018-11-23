﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using peekapi.Models;

namespace peekapi.Dao
{
    public class ComputadorDAO
    {
        public Computador PegarComputador(int idComputador)
        {

            using (SqlConnection cnx = new Banco().PegarConexao())
            {

                Computador c = new Computador();
                string sql = "SELECT * FROM PEEK_COMPUTADOR WHERE ID_COMPUTADOR = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@ID", idComputador);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            c.IdComputador = int.Parse(dr["ID_COMPUTADOR"].ToString());
                            c.QuantidadeMemoriaRam = dr["QUANTIDADE_MEMORIA_RAM"].ToString();
                            c.DescricaoProcessador = dr["DESCRICAO_PROCESSADOR"].ToString();
                            c.MacAddressInicial = dr["MAC_ADDRESS_INICIAL"].ToString();
                            c.IdLab = int.Parse(dr["ID_LAB"].ToString());

                            return c;
                        }

                    }


                }

                return null;

            }

        }

        public List<Computador> PegarTodosComputadores()
        {

            using (SqlConnection cnx = new Banco().PegarConexao())
            {
                {
                    List<Computador> listPc = new List<Computador>();
                    string sql = "SELECT * FROM PEEK_COMPUTADOR";
                    using (SqlCommand cmd = new SqlCommand(sql, cnx))
                    {

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Computador c = new Computador
                                {
                                    IdComputador = int.Parse(dr["ID_COMPUTADOR"].ToString()),
                                    QuantidadeMemoriaRam = dr["QUANTIDADE_MEMORIA_RAM"].ToString(),
                                    DescricaoProcessador = dr["DESCRICAO_PROCESSADOR"].ToString(),
                                    MacAddressInicial = dr["MAC_ADDRESS_INICIAL"].ToString()
                                };
                                // c.IdLab = int.Parse(dr["ID_LAB"].ToString());

                                listPc.Add(c);
                            }

                        }




                    }

                    return listPc;

                }
            }
        }

        public List<Computador> PegarTodosComputadoresByLab(int idLab)
        {
            using (SqlConnection cnx = new Banco().PegarConexao())
            {

                List<Computador> listPc = new List<Computador>();
                string sql = "SELECT * FROM PEEK_COMPUTADOR WHERE ID_LAB = @LAB";
                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@LAB", idLab);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            Computador c = new Computador
                            {
                                IdComputador = int.Parse(dr["ID_COMPUTADOR"].ToString()),
                                QuantidadeMemoriaRam = dr["QUANTIDADE_MEMORIA_RAM"].ToString(),
                                DescricaoProcessador = dr["DESCRICAO_PROCESSADOR"].ToString(),
                                MacAddressInicial = dr["MAC_ADDRESS_INICIAL"].ToString(),
                                IdLab = int.Parse(dr["ID_LAB"].ToString())
                            };

                            listPc.Add(c);
                        }

                    }


                }

                return listPc;


            }
        }

        public int PegarQuantidadeDePC(int idUsuario)
        {

            using (SqlConnection cnx = new Banco().PegarConexao())
            {
                Processador processador = new Processador();
                string sql = @"SELECT COUNT(PC.ID_COMPUTADOR) AS QNT_PC FROM PEEK_COMPUTADOR PC 
                                INNER JOIN PEEK_LAB L ON PC.ID_LAB = L.ID_LAB
                                INNER JOIN PEEK_USUARIO U ON U.ID_USUARIO = L.ID_USUARIO
                                WHERE U.ID_USUARIO = @ID;";
                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return int.Parse(dr["QNT_PC"].ToString());
                        }
                        return -1;
                    }


                }

            }

        }

    }
}