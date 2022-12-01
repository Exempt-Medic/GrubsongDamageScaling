using Modding;
using System;

namespace GrubsongDamageScaling
{
    public class GrubsongDamageScalingMod : Mod
    {
        private static GrubsongDamageScalingMod? _instance;

        internal static GrubsongDamageScalingMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(GrubsongDamageScalingMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public GrubsongDamageScalingMod() : base("GrubsongDamageScaling")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            ModHooks.TakeDamageHook += OnTakeDamage;

            Log("Initialized");
        }
        private int OnTakeDamage(ref int hazardType, int damage)
        {
            if (GameManager.instance.playerData.GetBool("overcharmed"))
            {
                damage *= 2;
            }
            if (BossSceneController.IsBossScene && BossSceneController.Instance.BossLevel == 1)
            {
                damage *= 2;
            }

            HeroController.instance.GRUB_SOUL_MP = 15;
            HeroController.instance.GRUB_SOUL_MP_COMBO = 25;
            HeroController.instance.GRUB_SOUL_MP *= damage;
            HeroController.instance.GRUB_SOUL_MP_COMBO *= damage;

            if (GameManager.instance.playerData.GetBool("overcharmed"))
            {
                damage /= 2;
            }
            if (BossSceneController.IsBossScene && BossSceneController.Instance.BossLevel == 1)
            {
                damage /= 2;
            }
            return damage;
        }
    }
}
