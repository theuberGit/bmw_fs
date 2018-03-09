using bmw_fs.Models.Showroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.Showroom
{
    interface ShowroomService
    {
        IList<ShowRoom> findAll(ShowRoom showroom);

        int findAllCount(ShowRoom showroom);

        void insertShowRoom(ShowRoom showroom);

        ShowRoom findShowRoom(ShowRoom showroom);

        void updateShowRoom(ShowRoom showroom);

        void deleteShowRoom(ShowRoom showroom);
    }
}
