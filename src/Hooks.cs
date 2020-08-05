using Harmony;
using System.Reflection;
using MelonLoader;

namespace AudicaModding
{
    internal static class Hooks
    {
        public static void ApplyHooks(HarmonyInstance instance)
        {
            instance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(MenuState), "SetState")]
        private static class PatchMenuSetState
        {
            private static void Postfix(MenuState.State state)
            {
                if(state == MenuState.State.LaunchPage && !AudicaMod.panelCreated)
                {
                    AudicaMod.CreatePanel();
                    return;
                }
                if (!AudicaMod.panelCreated) return;

                if (state == MenuState.State.LaunchPage) MelonCoroutines.Start(AudicaMod.SetPanelActive(true));
                else MelonCoroutines.Start(AudicaMod.SetPanelActive(false));
            }
        }
	}
}