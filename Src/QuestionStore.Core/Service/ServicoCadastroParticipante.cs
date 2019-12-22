﻿using QuestionStore.Core.Mapping;
using QuestionStore.Domain.Domain;
using System;
using System.Threading.Tasks;

namespace QuestionStore.Core.Service
{
    public class ServicoCadastroParticipante : IServiceParticipante
    {
        private readonly IMapper ParticipanteMapper;

        public ServicoCadastroParticipante(IMapper participanteMapper)
        {
            ParticipanteMapper = participanteMapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<Participante> Consulte()
        {
            return await Task.Run(() =>
            {
                return new Participante() { Nome = "Nasa!" };
            });
        }

        public async Task<bool> Insert(Command command)
        {
            return await Task.Run(() =>
            {
                ParticipanteMapper.Insert(command);
                return true;
            });
        }
    }


    public interface IServiceParticipante : IDisposable
    {
        Task<bool> Insert(Command command);

        Task<Participante> Consulte();
    }
}
