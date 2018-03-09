using bmw_fs.Models.board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.board
{
    interface BoardService
    {
       IList<Board> findAll(Board board);

       void insertBoard(HttpFileCollectionBase multipartFiles, Board board);

       Board findBoard(Board board);

       int findAllCount(Board board);

       void updateBoard(HttpFileCollectionBase multipartFiles, Board board);

       void deleteBoard(Board board);
        
    }
}
