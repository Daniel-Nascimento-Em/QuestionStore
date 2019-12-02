﻿using ControleFamiliar.Mapeadores;
using QuestionStore.Core.Service;
using QuestionStore.Domain.Domain;
using System;
using System.Collections.Generic;

namespace QuestionStore.Core.Mapping
{
    public class MapperAnswer
    {
        public MapperAnswer()
        {
        }

        public void Insert(InsertAnswerCommand insertAnswer)
        {
            using (var transacao = Connection.ObtenhaFbTransaction())
            using (var cmd = Connection.ObtenhaComando())
            {
                var ultimoCodigo = ObtenhaUltimoCodigo();
                cmd.CommandText = $@"INSERT INTO ANSWER (ANSCODIGO, ANSQSID, ANSLETRA) VALUES ('{ultimoCodigo}', '{insertAnswer.IdQuestion}', '{insertAnswer.IdAnswer}');";
                cmd.ExecuteNonQuery();

                transacao.Commit();
                transacao.Connection.Close();
            }

        }

        public List<Answer> GetAllAnswers()
        {
            var answerList = new List<Answer>();

            using (var transacao = Connection.ObtenhaFbTransaction())
            using (var cmd = Connection.ObtenhaComando())
            {
                cmd.CommandText = $@"SELECT ANSQSID, ANSLETRA FROM ANSWER";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        answerList.Add(new Answer
                        {
                            Question = dr.GetValue(0).ToString(),
                            Resposta = dr.GetValue(1).ToString()
                        });
                    }
                }

                transacao.Connection.Close();
            }

            return answerList;
        }

        private int ObtenhaUltimoCodigo()
        {
            using (var transacao = Connection.ObtenhaFbTransaction())
            using (var cmd = Connection.ObtenhaComando())
            {
                cmd.CommandText = $@"SELECT MAX(ANSWER.ANSCODIGO) FROM ANSWER";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.IsDBNull(0)) return 1;

                        return Convert.ToInt32(dr.GetValue(0)) + 1;
                    }

                }

                transacao.Connection.Close();
            }

            return 0;
        }
    }

}