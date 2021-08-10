// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Spider"",
            ""id"": ""c5790a12-8801-444a-9a2c-d2e54ae4679f"",
            ""actions"": [
                {
                    ""name"": ""WalkingX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""61c2b405-0e3d-4c0a-8833-007367487487"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""52d46789-bb41-439c-9663-811d50565f01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8fbdfa86-6659-44b7-997a-4e7bb8138f1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swing"",
                    ""type"": ""Button"",
                    ""id"": ""0eff3214-7e6b-4aa5-99e0-dc87a237a8e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0eeecdbe-5a3f-416d-b9dc-9906da74be9f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6bb967af-8c31-489e-9c89-c6b2c5daef88"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WalkingX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cdae45a4-f3ee-4f3a-91f6-e30d0f9d8af9"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""WalkingX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""12258643-d81c-4ff9-8c1e-a888622bcb58"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""WalkingX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fdfff4da-bbfb-4b4c-8854-95fd20089a9e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""WalkingX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""15a44ae9-0bb3-42a4-837e-beff630a3f70"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""WalkingX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3453ba95-a3f3-46d2-8fe3-2d27ddd39d0e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0159b16-2447-4df3-bef6-9a1f47e4bd85"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bb67c72-dde1-4e0f-b894-5c6f79759cfc"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f8539a5e-bb9c-4925-a9c0-f2ae02eab7ad"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""473caecf-69be-49c2-9e6f-768ef3805d93"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Reel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""22037eaa-ffb7-484b-a353-01525e361c78"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Reel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XBox Control Scheme"",
            ""bindingGroup"": ""XBox Control Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Spider
        m_Spider = asset.FindActionMap("Spider", throwIfNotFound: true);
        m_Spider_WalkingX = m_Spider.FindAction("WalkingX", throwIfNotFound: true);
        m_Spider_RotateCamera = m_Spider.FindAction("RotateCamera", throwIfNotFound: true);
        m_Spider_Jump = m_Spider.FindAction("Jump", throwIfNotFound: true);
        m_Spider_Swing = m_Spider.FindAction("Swing", throwIfNotFound: true);
        m_Spider_Reel = m_Spider.FindAction("Reel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Spider
    private readonly InputActionMap m_Spider;
    private ISpiderActions m_SpiderActionsCallbackInterface;
    private readonly InputAction m_Spider_WalkingX;
    private readonly InputAction m_Spider_RotateCamera;
    private readonly InputAction m_Spider_Jump;
    private readonly InputAction m_Spider_Swing;
    private readonly InputAction m_Spider_Reel;
    public struct SpiderActions
    {
        private @InputMaster m_Wrapper;
        public SpiderActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @WalkingX => m_Wrapper.m_Spider_WalkingX;
        public InputAction @RotateCamera => m_Wrapper.m_Spider_RotateCamera;
        public InputAction @Jump => m_Wrapper.m_Spider_Jump;
        public InputAction @Swing => m_Wrapper.m_Spider_Swing;
        public InputAction @Reel => m_Wrapper.m_Spider_Reel;
        public InputActionMap Get() { return m_Wrapper.m_Spider; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpiderActions set) { return set.Get(); }
        public void SetCallbacks(ISpiderActions instance)
        {
            if (m_Wrapper.m_SpiderActionsCallbackInterface != null)
            {
                @WalkingX.started -= m_Wrapper.m_SpiderActionsCallbackInterface.OnWalkingX;
                @WalkingX.performed -= m_Wrapper.m_SpiderActionsCallbackInterface.OnWalkingX;
                @WalkingX.canceled -= m_Wrapper.m_SpiderActionsCallbackInterface.OnWalkingX;
                @RotateCamera.started -= m_Wrapper.m_SpiderActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_SpiderActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_SpiderActionsCallbackInterface.OnRotateCamera;
                @Jump.started -= m_Wrapper.m_SpiderActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_SpiderActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_SpiderActionsCallbackInterface.OnJump;
                @Swing.started -= m_Wrapper.m_SpiderActionsCallbackInterface.OnSwing;
                @Swing.performed -= m_Wrapper.m_SpiderActionsCallbackInterface.OnSwing;
                @Swing.canceled -= m_Wrapper.m_SpiderActionsCallbackInterface.OnSwing;
                @Reel.started -= m_Wrapper.m_SpiderActionsCallbackInterface.OnReel;
                @Reel.performed -= m_Wrapper.m_SpiderActionsCallbackInterface.OnReel;
                @Reel.canceled -= m_Wrapper.m_SpiderActionsCallbackInterface.OnReel;
            }
            m_Wrapper.m_SpiderActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WalkingX.started += instance.OnWalkingX;
                @WalkingX.performed += instance.OnWalkingX;
                @WalkingX.canceled += instance.OnWalkingX;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Swing.started += instance.OnSwing;
                @Swing.performed += instance.OnSwing;
                @Swing.canceled += instance.OnSwing;
                @Reel.started += instance.OnReel;
                @Reel.performed += instance.OnReel;
                @Reel.canceled += instance.OnReel;
            }
        }
    }
    public SpiderActions @Spider => new SpiderActions(this);
    private int m_XBoxControlSchemeSchemeIndex = -1;
    public InputControlScheme XBoxControlSchemeScheme
    {
        get
        {
            if (m_XBoxControlSchemeSchemeIndex == -1) m_XBoxControlSchemeSchemeIndex = asset.FindControlSchemeIndex("XBox Control Scheme");
            return asset.controlSchemes[m_XBoxControlSchemeSchemeIndex];
        }
    }
    public interface ISpiderActions
    {
        void OnWalkingX(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwing(InputAction.CallbackContext context);
        void OnReel(InputAction.CallbackContext context);
    }
}
