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

        bool Insert(State state);

        bool update(State state);

        bool delete(int stateId);

        List<State> GetAll();

        State GetById(int id);
    }
}
