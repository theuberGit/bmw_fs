using bmw_fs.Common.Filter;
using bmw_fs.Models.board;
using bmw_fs.Service.face.board;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.board;
using bmw_fs.Service.impl.common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.common
{
    [Authorize]
    [TestFilter]
    public class BoardController : Controller
    {
        BoardService boardSerivce = new BoardServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesServce = new FilesServiceImpl();

        public ActionResult list(Board board)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(board, 10, boardSerivce.findAllCount(board));
            ViewBag.list = boardSerivce.findAll(board);
            ViewBag.pagination = board;
            
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Board board)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            board.regId = System.Web.HttpContext.Current.User.Identity.Name;
            boardSerivce.insertBoard(multipartRequest, board);

         
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }
       
        public ActionResult view(Board board)
        {
            Board item = boardSerivce.findBoard(board);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndType(item.idx, "files");
            ViewBag.filesList2 = filesServce.findAllByMasterIdxAndType(item.idx, "files2");

            return View();
        }

        public ActionResult modify(Board board)
        {
            Board item = boardSerivce.findBoard(board);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndTypeForUpload(item.idx, "files");
            ViewBag.filesList2 = filesServce.findAllByMasterIdxAndTypeForUpload(item.idx, "files2");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Board board)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            board.updateId = System.Web.HttpContext.Current.User.Identity.Name;
            boardSerivce.updateBoard(multipartRequest, board);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Board board)
        {
            boardSerivce.deleteBoard(board);
            return RedirectToAction("list");
        }

    }
}