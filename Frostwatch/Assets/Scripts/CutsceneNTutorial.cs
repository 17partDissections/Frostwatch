using Q17pD.Frostwatch.Infrastructure;
using Q17pD.Frostwatch.Interactive;
using Q17pD.Frostwatch.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Q17pD.Frostwatch
{
    public class CutsceneNTutorial : MonoBehaviour
    {
        [SerializeField] private AudioClip _fireIgnite;
        [SerializeField] private PlayerRotateButton _leftButton, _rightButton;
        private bool _isCampaign;
        private UIHighlight _darkeningPanel;
        private AudioHandler _audioHandler;
        private CursorHandler _cursorHandler;


        [Inject] private void Construct(Player.Player player, AudioHandler audioHandler, CursorHandler cursorHandler, bool isCampaign)
        {
            _isCampaign = isCampaign;
            _darkeningPanel = player.PlayerCanvasHandler.DarkeningPanel.GetComponent<UIHighlight>();
            _darkeningPanel.gameObject.SetActive(true);
            _audioHandler = audioHandler;
            _audioHandler.gameObject.SetActive(false);
            _cursorHandler = cursorHandler;
            _cursorHandler.LockCursorToggle(true);

        }

        private IEnumerator Start()
        {
            BranchHandler branchHandler = GetComponent<BranchHandler>();
            branchHandler.enabled = !_isCampaign; _leftButton.enabled = !_isCampaign; _rightButton.enabled = !_isCampaign;
            yield return new WaitForSeconds(2);
            _audioHandler.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            _audioHandler.PlaySFX(_fireIgnite, 0);
            yield return new WaitForSeconds(_fireIgnite.length - (_fireIgnite.length/4));
            _darkeningPanel.UnHighlightImage(_fireIgnite.length / 4);
            yield return new WaitForSeconds(_fireIgnite.length / 4);
            _darkeningPanel.gameObject.SetActive(false);
            _rightButton.Rotate();
            yield return new WaitForSeconds(0.5f);
            var interactive = GetComponent<GameSceneInstaller>().Interactive;
            foreach (var obj in interactive) obj.enabled = true;
            _cursorHandler.LockCursorToggle(false);
            if (!_isCampaign) GetComponent<MonstersHandler>().StartMonstersCoroutine();
        }
    }
}
