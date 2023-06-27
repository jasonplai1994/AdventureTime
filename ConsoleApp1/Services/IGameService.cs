using ConsoleApp1.Models;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public interface IGameService
    {
        //method headers here follower by ;
        public ErrorOr<Created> CreateGame(Game game);
    }
}
