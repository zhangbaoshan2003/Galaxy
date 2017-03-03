using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.DAL.Model;

namespace Galaxy.DAL.DataContext
{
    public class GalaxyDBInitializer : System.Data.Entity.DropCreateDatabaseAlways<GalaxyDB>
    {
        private Random random = new Random(7);

        public void generateNetValue(Product product,GalaxyDB context)
        { 
            for (int i = 0; i < 250; i++)
            {
                ProductNetValue productNetValue = new ProductNetValue();
                double netvalue = random.NextDouble();
                while (netvalue < 0.7)
                {
                    netvalue = random.NextDouble();
                }

                productNetValue.ProductID = product.ID;
                productNetValue.UpdatedDate = DateTime.Today.AddDays(i);
                productNetValue.NetValue = netvalue;
                context.ProductNetValues.Add(productNetValue);
            }
        }

        

        private Product buildProduct(int id,String proName)
        {
            Product product = new Product()
            {
                ID=id,
                Name = proName,//"元达信资本-履泰2期证券投资基金",
                AdministratorID = 1,
                ConsultentID = 1,
                ProductTypeID = 1,
                CustodyBank = "中信建投证券",
                Broker = "中信建投证券",
                IssueDate = DateTime.Today.AddYears(-1),
                LifeTime = 20,
                InitialInvestment = 1000000000,
                ProportionOfConsultant = 0.15,
                MinimumBuy = 1000000,
                StepBuy = 100000,
                ClosureTerm = "1年,第一个开放日之后，产品转为管理型",
                OpenTerm = "封闭期后每季度X日开放，可申购赎回。（遇节假日顺延）",
                SubscriptionFee = 0,
                RedemptionFee = 0,
                ManagementFee = 0.003,
                WayToCalcManagementFee = "按照前一日的计划资产净值，按日计提，按季支付",
                BenchmarkToRewardConsultant = "封闭期届满后的第一个开放日，计提净值超过1以上的30%，转管理型后提取超额业绩的20%",
                WayToCollectReward = "逢开放日/产品终止清算日计提",
                WayToReceiveBonus = "无",
                ThresholdOfWarning = 0.9,
                WarningDesc =
                    "0.90（产品净值触及预警线或在预警线以下，投顾指定投资者可追加增强资金，使产品净值回到预警线以上，若投顾方选择不追加增强资金或追加增强资金后产品净值仍低于0.95，管理人会将风险资产仓位控制在50%以下）",
                TresholdOfStopLoss = 0.87,
                StopLossDesc =
                    "0.87（在本资管计划存续期内任何一个工作日（T日）收盘后，经管理人估算的基金单位净值不高于止损线的，投顾方有权选择于t+1日10：00前追加增强资金，使产品净值高于止损线，否则管理人强制平仓止损）",
                SecurityTypeToInvest = "主要投资于沪、深交易所A股股票、沪港通、基金、债券、新股申购（网下新股申购除外）、银行存款等。",
                ConstrainsOfInvestment =
                    "1. 本基金投资于一家上市公司所发行的股票，不得超过该上市公司总股本的4.99%，同时不得超过该上市公司流通股本的10%；2. 购买单一非创业板公司股票不超过基金总资产的40％；购买单一创业板公司股票不超过基金总资产的20％，所有投于创业板的股票不超过基金总资产的30%。3. 不得投资于ST、*ST、S类股票；4. 不得投资于权证。5. 不得购买管理人自身发行的股票以及与管理人存在关联关系的上市公司股票。",
                StrategyTypeID = 1
            };
            return product;
        }

        protected override void Seed(GalaxyDB context)
        {
            var administrators = new List<Administrator>
            {
                new Administrator(){ID = 1,Name = "元达信资本"},
                new Administrator(){ID = 2,Name = "冠石泽惠"},
                new Administrator(){ID = 3,Name = "Admin3"},
                new Administrator(){ID = 4,Name = "Admin4"}
            };

            administrators.ForEach(a =>
            {
                context.Administrators.Add(a);
            });

            var consultents = new List<Consultant>
            {
                new Consultant(){ID=1,Name = "天津履泰投资有限公司"},
                new Consultant(){ID=2,Name = "因诺资产"},
                new Consultant(){ID=3,Name = "易方达量化投资"},
                new Consultant(){ID=4,Name = "九天量化"},
            };
            consultents.ForEach(c =>
            {
                context.Consultants.Add(c);
            });
            context.SaveChanges();
            
            var prodcutTypes = new List<ProductType>
            {
                new ProductType(){ID = 1,Name = "风险缓冲转管理型"},
                new ProductType(){ID = 2,Name = "市场中性量化对称型"},
                new ProductType(){ID = 3,Name = "价值投资"},
                new ProductType(){ID = 4,Name = "统计套利型"},
            };
            prodcutTypes.ForEach(p =>
            {
                context.ProductTypes.Add(p);
            });

            var strategyTypes = new List<StrategyType>
            {
                new StrategyType(){ID = 1,Name = "策略1"},
                new StrategyType(){ID = 2,Name = "策略2"},
                new StrategyType(){ID = 3,Name = "策略3"},
                new StrategyType(){ID = 4,Name = "策略4"}
            };
            strategyTypes.ForEach(x =>
            {
                context.StrategyTypes.Add(x);
            });
            context.SaveChanges();

            for (int i = 0; i < 50; i++)
            {
                Product product = buildProduct(i+1,String.Format("元达信资本-履泰{0}期证券投资基金",i.ToString()));
                generateNetValue(product, context);
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
