using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.MyMotion
{
    public class LSParamCommonInit
    {
        public static List<HomeModel> homeModels = new List<HomeModel>();

        public static List<StopModelEnum> stopModels = new List<StopModelEnum>();

        public static List<CoordModelEnum> coordModels = new List<CoordModelEnum>();

        public static List<HomeSpeedModel> homeSpeeds = new List<HomeSpeedModel>();

        public static void InitAxiasEnums()
        {
            InitHomeModel();
            InitStopModel();
            InitCoordModel();
            InitHomeSpeedModel();
        }

        private static List<HomeModel> InitHomeModel()
        {
            homeModels.Add(HomeModel.一次回零);
            homeModels.Add(HomeModel.一次回零加反找);
            homeModels.Add(HomeModel.两次回零);
            homeModels.Add(HomeModel.一次回零加EZ);
            homeModels.Add(HomeModel.EZ回零);
            homeModels.Add(HomeModel.一次回零加反找EZ);
            homeModels.Add(HomeModel.原点锁存);
            homeModels.Add(HomeModel.原点加EZ锁存);
            homeModels.Add(HomeModel.EZ锁存);
            homeModels.Add(HomeModel.原点加反向EZ锁存);
            homeModels.Add(HomeModel.限位一次回零);
            homeModels.Add(HomeModel.限位回零反找);
            homeModels.Add(HomeModel.限位两次回零);
            return homeModels;
        }

        private static List<StopModelEnum> InitStopModel()
        {
            stopModels.Add(StopModelEnum.减速停止);
            stopModels.Add(StopModelEnum.立即停止);
            return stopModels;
        }

        private static List<CoordModelEnum> InitCoordModel()
        {
            coordModels.Add(CoordModelEnum.相对坐标);
            coordModels.Add(CoordModelEnum.绝对坐标);
            return coordModels;
        }

        private static List<HomeSpeedModel> InitHomeSpeedModel()
        {
            homeSpeeds.Add(HomeSpeedModel.低速回零);
            homeSpeeds.Add(HomeSpeedModel.高速回零);
            return homeSpeeds;
        }
    }
}
