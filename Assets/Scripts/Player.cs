using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class Player : MonoBehaviour
    {
    #region Attributes
        [SerializeField]
        private GameManager m_gameManager;

        private string  m_sName;
        private int     m_iMoney;

        private Dictionary<int, int> m_liIncomeSources = new Dictionary<int, int>();

        // Is the player controlled by A.I.
        [SerializeField]
        private bool m_bIsAI;

        // Camera components
        private Camera m_cam = null;
        private AudioListener m_listner = null;

        // Camera move speed
        [SerializeField, Range(10.0f, 20.0f)]
        private float m_fCamSpeed = 10.0f;
    #endregion // Attributes

    #region Accessors
        public int Money { get => m_iMoney; set => m_iMoney = value; }

        public Camera Cam { get => m_cam; set => m_cam = value; }

        public float CamSpeed { get => m_fCamSpeed; set => m_fCamSpeed = value; }

        public bool isAI { get => m_bIsAI; set => m_bIsAI = value; }
    #endregion

    #region Functions
        void Awake()
        {
            m_sName = gameObject.name;

            m_cam = gameObject.GetComponent<Camera>();
            m_cam.orthographicSize = 5;
            m_cam.enabled = false;
            m_listner = gameObject.GetComponent<AudioListener>();
            m_listner.enabled = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            // set the default camera settings
            //gameObject.transform.position = new Vector3(-columns / 2 * (size + padding), -rows / 2 * (size + padding), 5);
        }

        // Update is called once per frame
        public void PlayerUpdate()
        {
            CameraInputs();

            // ControlInputs();
        }

        // Enable/Disable Player's Camera
        public void UpdateCamState()
        {
            if (m_cam)
                m_cam.enabled = !m_cam.enabled;
            if (m_listner)
                m_listner.enabled = !m_listner.enabled;
        }

        public void AddGains()
        {
            if (m_liIncomeSources.Count > 0)
            {
                foreach (var incomeSource in m_liIncomeSources)
                {
                    m_iMoney += incomeSource.Key * incomeSource.Value;
                    Debug.Log(incomeSource.Key * incomeSource.Value);
                }
            }
        }

        // Handle start of the player's turn
        public void StartTurn()
        {
            Debug.Log("Beginning of " + m_sName + "'s turn.");

            UpdateCamState();

            AddGains();
        }
        // Handle end of the player's turn
        public void EndTurn()
        {
            UpdateCamState();

            Debug.Log("End of " + m_sName + "'s turn.");
        }


        // Handle Camera controls
        void CameraInputs()
        {
            MoveCam();
            Zoom();
        }

        // Handle the movement of the camera
        void MoveCam()
        {
            Vector3 pos = gameObject.transform.position;
            float frustrumSize = m_cam.orthographicSize;

            if (Input.GetKey("up"))
                pos.y = Mathf.Min((pos.y + m_fCamSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("down"))
                pos.y = Mathf.Max((pos.y - m_fCamSpeed * Time.deltaTime), -(m_gameManager.Map.CellSize * m_gameManager.Map.Height));

            if (Input.GetKey("left"))
                pos.x = Mathf.Min((pos.x + m_fCamSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("right"))
                pos.x = Mathf.Max((pos.x - m_fCamSpeed * Time.deltaTime), -(m_gameManager.Map.CellSize * m_gameManager.Map.Width));

            gameObject.transform.position = pos;
        }
        // Handle the zoom of the camera
        void Zoom()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // closer
                m_cam.orthographicSize = Mathf.Max(m_cam.orthographicSize-1, 4);
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // further
                m_cam.orthographicSize = Mathf.Min(m_cam.orthographicSize+1, 8);
        }
    #endregion // Functions
    }
}