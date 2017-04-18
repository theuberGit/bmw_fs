using bmw_fs.Models.board;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.board
{
    public class BoardDao
    {
        public IList<Board> findAll(Board board)
        {
            return Mapper.Instance().QueryForList<Board>("board.findAll", board);
        }

        public void insertBoard(Board board)
        {
            Mapper.Instance().Insert("board.insertBoard", board);
        }

        public Board findBoard(Board board)
        {
            return Mapper.Instance().QueryForObject<Board>("board.findBoard", board);
        }

        public int findAllCount(Board board)
        {
            return Mapper.Instance().QueryForObject<int>("board.findAllCount", board);
        }

        public void updateBoard(Board board)
        {
            Mapper.Instance().Update("board.updateBoard", board);
        }

        public void deleteBoard(Board board)
        {
            Mapper.Instance().Delete("board.deleteBoard", board);
        }
    }
}