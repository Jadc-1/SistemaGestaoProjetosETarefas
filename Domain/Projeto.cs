﻿using SistemaGestaoProjetosETarefas.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Projeto
    {
        private static int _IdIncremento = 1;
        public int CodigoDoProjeto { get; private set; }
        public string? Nome { get; set; }
        public string? Desc { get; set; }
        public List<Tarefa>? Tarefas { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public Status? StatusProjeto { get; set; }
        public List<Status>? StatusTarefas { get; set; }
        public char Prioridade { get; set; }
        public Gestor? GestorDelegado { get; set; }

        public Projeto(string nome, string desc, DateTime dataInicio, char prioridade)
        {
            CodigoDoProjeto = _IdIncremento++;
            this.Nome = nome;
            this.Desc = desc;
            this.Tarefas = new List<Tarefa>();
            this.StatusTarefas = new List<Status>();
            this.StatusProjeto = Status.EmAndamento;
            this.DataTermino = default; // Inicializa a data de término como padrão (DateTime.MinValue)
            this.DataInicio = dataInicio;
            this.Prioridade = prioridade;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if(tarefa != null)
            {
                Tarefas?.Add(tarefa);
            }
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            if (tarefa != null && Tarefas != null)
            {
                Tarefas.Remove(tarefa);
            }
        }

        public void AlterarPrioridade(char prioridade)
        {
            if (prioridade != '\0') // Verifica se a prioridade não é nula
            {
                this.Prioridade = prioridade;
            }
        }

        public void AtribuirGestor(Gestor gestor)
        {
            if (gestor != null)
            {
                this.GestorDelegado = gestor;
                gestor.ProjetosGerenciados.Add(this);
            }
        }

        public void RemoverGestor()
        {
            if (GestorDelegado != null)
            {
                this.GestorDelegado = null;
            }
            else
            {
                Console.WriteLine("Nenhum gestor atribuído ao projeto.");
            }
        }

        public void FinalizarProjeto()
        {
            this.StatusProjeto = Status.Concluido;
            this.DataTermino = DateTime.Now;
        }

        public void CancelarProjeto()
        {
            this.StatusProjeto = Status.Cancelado;
            this.DataTermino = DateTime.Now;
        }

        public override string ToString()
        {
            return "Projeto: " + Nome + "\n" +
                   "Descrição: " + Desc + "\n" +
                   "Data de Início: " + DataInicio.ToString("dd/MM/yyyy") + "\n" +
                   "Status: " + StatusProjeto?.Categoria + "\n" +
                   "Prioridade: " + Prioridade + "\n" +
                   "Gestor Delegado: " + GestorDelegado?.Nome;
        }
    }
}
