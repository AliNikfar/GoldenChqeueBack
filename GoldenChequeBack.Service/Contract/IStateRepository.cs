using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IStateRepository
    {

        State Insert(State state);

        State update(State state);

        bool delete(int stateId);

        List<State> GetAll();

        State GetById(int id);
    }
}
