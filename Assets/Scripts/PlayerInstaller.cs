using Cinemachine;
using Entities.Player;
using GUI;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _position;

    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private GuiHandler _guiHandler;
    
    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, 
            _position, Quaternion.identity, null);

        Container.Bind<Player>().FromInstance(player).AsSingle();
        player.GetComponent<Player>().enabled = true;
        player.Gui = _guiHandler;

        var camera = _cameraPrefab.GetComponentInChildren<CinemachineFreeLook>();
        camera.Follow = player.transform;
        camera.LookAt = player.transform.GetChild(0);
    }
}