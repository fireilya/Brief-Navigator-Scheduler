using System;
using System.Text;
using Domain.Scheduler;
using Scheduler.Data;
using Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Scheduler.Wrappers
{
    public abstract class SubtaskWrapper
    {
        public Subtask Subtask { get; set; }
        public UnityEvent<Guid> OnToolChanges = new();

        protected SubtaskWrapper(Subtask subtask)
        {
            Subtask = subtask;
        }

        public bool IsCapacityTask => Subtask.IsUseCapacityTool;
        public string SubtaskName => Subtask.Name;

        public int SubtaskOrder => Subtask.Order;
        public abstract string SubtaskToolInfo { get; }

        public Guid ChosenToolId
        {
            get => _chosenToolId;
            set
            {
                if (value == _chosenToolId) return;
                _chosenToolId = value;
                OnToolChanges.Invoke(value);
            }
        }

        private Guid _chosenToolId = Guid.Empty;

        public Guid ChosenNeutralizerId { get; set; } = Guid.Empty;

        public int PrecalculatedProgress { get; protected set; } = 0;

        public RiskInfluenceMark ApplyRiskInfluence()
        {
            var answer = new RiskInfluenceMark();
            var risk = DataContainer.CurrentActionArea.GetLocationById(Subtask.LocationID).Risk;
            answer.IsHappened = risk.TryRiskHappened(ChosenNeutralizerId, out answer.InfluenceCoeff);
            if (answer.IsHappened) answer.RiskMessage = BuildHappenedRiskMessage(risk);
            return answer;
        }

        private string BuildHappenedRiskMessage(Risk risk)
        {
            var sb = new StringBuilder();
            sb.Append($"Внимание! {risk.Name}!\n\n");
            sb.Append($"При выполнении задачи \"{SubtaskName}\" {risk.HappenedMessage.ToLower()}");

            if (!risk.IsRightNeutralizer(ChosenNeutralizerId))
            {
                sb.Append(ChosenNeutralizerId == Guid.Empty
                    ? " и у работника не было экипировки. "
                    : $" и экипировка \"{DBServerMock.GetNeutralizer(ChosenNeutralizerId).Name}\" не спасла его. ");
                sb.Append(risk.BadInfluenceMessage);
                sb.Append(" Эффективность работы была снижена.");
            }

            else
            {
                var neutralizer = DBServerMock.GetNeutralizer(ChosenNeutralizerId);
                sb.Append(
                    $", но экипировка \"{neutralizer.Name}\" спасла работника. Эффективность работы не пострадала.");
            }
            
            return sb.ToString();
        }


        public void ResetPrecalculatedProgress() => PrecalculatedProgress = 0;
        public abstract void PrecalculateSubtaskProgress(float workerEfficiencyCoeff, bool isLastTaskCell);

        public abstract int CalculateReferenceProgress(int scheduledOnHours);

        public abstract int SubtaskEfficiency { get; }

        public void ResetChosenTool() => ChosenToolId = Guid.Empty;

        public void ResetChosenNeutralizer() => ChosenNeutralizerId = Guid.Empty;
    }
}