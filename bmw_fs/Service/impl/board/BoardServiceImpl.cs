using bmw_fs.Dao.face.board;
using bmw_fs.Models.board;
using bmw_fs.Service.face.board;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.board
{
    public class BoardServiceImpl : BoardService
    {
        BoardDao boardDao = new BoardDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<Board> findAll(Board board)
        {
            return boardDao.findAll(board);
        }

        public void insertBoard(HttpFileCollectionBase multipartFiles, Board board)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            board.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            boardDao.insertBoard(board);
            filesService.fileUpload(multipartFiles, "files" , "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            filesService.fileUpload(multipartFiles, "files2" , "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public Board findBoard(Board board)
        {
            return boardDao.findBoard(board);
        }

        public int findAllCount(Board board)
        {
            return boardDao.findAllCount(board);
        }

        public void updateBoard(HttpFileCollectionBase multipartFiles, Board board)
        {
            Mapper.Instance().BeginTransaction();
            filesService.deleteFileAndFileUpload(multipartFiles, "files", "jpg|png", 5 * 1024 * 1024, board.idx, board.fileIdxs);
            filesService.deleteFileAndFileUpload(multipartFiles, "files2", "jpg|png", 5 * 1024 * 1024, board.idx, board.fileIdxs2);
            boardDao.updateBoard(board);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteBoard(Board board)
        {
            Mapper.Instance().BeginTransaction();
            boardDao.deleteBoard(board);
            filesService.deleteRealFilesAndDataByFileMasterIdx(board.idx);
            Mapper.Instance().CommitTransaction();
        }
    }
}