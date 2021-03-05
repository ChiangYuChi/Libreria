using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Service
{
    public class ShoppingService
    {
        private readonly LibreriaRepository _DbRepository;

        public ShoppingService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<ShoppingCartViewModel> GetAll()
        {
            var MemberId = Convert.ToInt32(HttpContext.Current.Session["MemberID"]);

            var result = (from s in _DbRepository.GetAll<ShoppingCart>()
                          join p in _DbRepository.GetAll<Product>()
                          on s.ProductId equals p.ProductId
                          where s.memberId == MemberId
                          join v in _DbRepository.GetAll<Preview>()
                          on s.ProductId equals v.ProductId
                          where v.Sort == 0
                          select new ShoppingCartViewModel()
                          {
                              ProductId = s.ProductId,
                              ProductName = p.ProductName,
                              Count = s.Count,
                              Price = p.UnitPrice,
                              PicUrl = v.ImgUrl
                          }).ToList();

            return result;
        }
        /// <summary>
        /// 購物車取得senssion之資料
        /// </summary>
        /// <returns>
        /// 回傳為集合
        /// </returns>
        public List<ShoppingCartViewModel> GetAnonymousAll()
        {
            List<ShoppingCart> cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];

            if (cartitems == null)
            {
                List<ShoppingCart> SessionCart = new List<ShoppingCart>(); //为了Session弄得
                HttpContext.Current.Session["ShoppingCart"] = SessionCart;
                var result = new List<ShoppingCartViewModel>();
                return result;
            }
            else
            {
                var result = (from s in cartitems
                              join p in _DbRepository.GetAll<Product>()
                              on s.ProductId equals p.ProductId
                              join v in _DbRepository.GetAll<Preview>()
                              on s.ProductId equals v.ProductId
                              where v.Sort == 0
                              select new ShoppingCartViewModel()
                              {
                                  ProductId = s.ProductId,
                                  ProductName = p.ProductName,
                                  Count = s.Count,
                                  Price = p.UnitPrice,
                                  PicUrl = v.ImgUrl
                              }).ToList();

                return result;
            }
        }

        public OperationResult AddToCart(int productId)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (MemberId == 0)
                {
                    List<ShoppingCart> cartitems;

                    if (HttpContext.Current.Session["ShoppingCart"] == null)
                    {
                        cartitems = new List<ShoppingCart>();
                        ShoppingCart SessionEntity = new ShoppingCart() { ProductId = productId, memberId = 0, Count = 1 };
                        cartitems.Add(SessionEntity);
                        HttpContext.Current.Session["ShoppingCart"] = cartitems;
                    }
                    else
                    {
                        cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];
                        if (cartitems.Where(x => x.ProductId == productId).FirstOrDefault() == null)
                        {
                            ShoppingCart SessionEntity = new ShoppingCart() { ProductId = productId, memberId = 0, Count = 1 };
                            cartitems.Add(SessionEntity);
                        }
                        HttpContext.Current.Session["ShoppingCart"] = cartitems;
                    }
                }
                else
                {
                    if (_DbRepository.GetAll<ShoppingCart>().Where(x => x.memberId == MemberId && x.ProductId == productId).FirstOrDefault() == null)
                    {
                        ShoppingCart entity = new ShoppingCart() { ProductId = productId, memberId = MemberId, Count = 1 };
                        _DbRepository.Create<ShoppingCart>(entity);
                    }
                }

                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult DeleteFromCart(int productId)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (MemberId == 0)
                {
                    List<ShoppingCart> cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];
                    var newcart = cartitems.Where(x => x.ProductId != productId).ToList();
                    HttpContext.Current.Session["ShoppingCart"] = newcart;
                }
                else
                {
                    _DbRepository.Delete<ShoppingCart>(_DbRepository.GetAll<ShoppingCart>()
                        .Where(x => x.ProductId == productId && x.memberId == MemberId)
                        .FirstOrDefault());
                }

                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult AddOne(int productId)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (MemberId == 0)
                {
                    List<ShoppingCart> cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];
                    var newcart = cartitems
                        .Select(x => x.ProductId == productId ? new ShoppingCart { ProductId = x.ProductId, Count = x.Count + 1, memberId = 0 } : x)
                        .ToList();

                    HttpContext.Current.Session["ShoppingCart"] = newcart;
                }
                else
                {
                    ShoppingCart entity = _DbRepository.GetAll<ShoppingCart>()
                        .Where(x => x.ProductId == productId && x.memberId == MemberId)
                        .FirstOrDefault();

                    entity.Count += 1;
                    _DbRepository.Update<ShoppingCart>(entity);
                }

                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult MinusOne(int productId)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            try
            {
                if (MemberId == 0)
                {
                    List<ShoppingCart> cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];
                    var newcart = cartitems
                        .Select(x => x.ProductId == productId ? new ShoppingCart { ProductId = x.ProductId, Count = x.Count - 1, memberId = 0 } : x)
                        .Where(x => x.Count > 0)
                        .ToList();

                    HttpContext.Current.Session["ShoppingCart"] = newcart;
                }
                else
                {
                    ShoppingCart entity = _DbRepository.GetAll<ShoppingCart>()
                        .Where(x => x.ProductId == productId && x.memberId == MemberId)
                        .FirstOrDefault();

                    if (entity.Count - 1 != 0)
                    {
                        entity.Count -= 1;
                        _DbRepository.Update<ShoppingCart>(entity);
                    }
                }

                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public int Redirect()
        {
            if (HttpContext.Current.Session["MemberId"] != null)
            {
                return 1; //会员已登录
            }
            else
            {
                return 2; //会员没有登陆
            }
        }

        public void CombineCarts()
        {
            var MemberId = Convert.ToInt32(HttpContext.Current.Session["MemberID"]);

            if (HttpContext.Current.Session["ShoppingCart"] != null)
            {
                List<ShoppingCart> cartitems = (List<ShoppingCart>)HttpContext.Current.Session["ShoppingCart"];

                foreach (var item in cartitems)
                {
                    if (_DbRepository.GetAll<ShoppingCart>().Where(x => x.memberId == MemberId && x.ProductId == item.ProductId).FirstOrDefault() == null)
                    {
                        ShoppingCart entity = new ShoppingCart { ProductId = item.ProductId, memberId = MemberId, Count = item.Count };
                        _DbRepository.Create<ShoppingCart>(entity);
                    }
                }

                HttpContext.Current.Session["ShoppingCart"] = null;
            }
        }
    }
}