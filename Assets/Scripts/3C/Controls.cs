using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent m_nav_mesh_agent;

    [SerializeField]
    private GameObject m_fire_camp;

    [SerializeField]
    private GameObject m_pop_up_confirm;

    private ItemHolder m_destination_pickup_;
    private GameObject m_current_selected_object_;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.GetInstance().OnClickLeftMouseButton += OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.z * 100f) * -1;
    }

    private void OnClickLeftMouseHandler()
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, 1000, mask))
            m_nav_mesh_agent.SetDestination(hit.point);*/
    }

    private void OnClickRightMouseHandler()
    {
        var Player = GetGameObjectInRaycastAllByTag("Player");
        if (Player != null)
        {
            m_current_selected_object_ = Player;
            return;
        }

        // todo 
        var Item = GetItem();
        if (Item == null)
            Item = GetItem();

        if (m_current_selected_object_ != null && Item != null)
        {
            m_pop_up_confirm.GetComponent<AnchoredSpriteUI>().target = Item.transform;
            m_destination_pickup_ = Item;
            m_pop_up_confirm.SetActive(true);
            return;
        }
        this.m_current_selected_object_ = null;
    }

    private void OnDestroy()
    {
        InputManager.GetInstance().OnClickLeftMouseButton -= OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton -= OnClickRightMouseHandler;
    }

    private GameObject GetGameObjectInRaycastAllByTag([NotNull] string _sTag)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] RayCastAll = Physics.RaycastAll(ray, 100);
        for (int i = 0; i < RayCastAll.Length; i++)
        {
            if (RayCastAll[i].transform.gameObject.CompareTag(_sTag))
            {
                return RayCastAll[i].transform.gameObject;
            }
        }
        return null;
    }

    private ItemHolder GetItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] RayCastAll = Physics.RaycastAll(ray, 100);
        ItemHolder item;
        for (int i = 0; i < RayCastAll.Length; i++)
        {
            item = RayCastAll[i].transform.gameObject.GetComponent<ItemHolder>();
            if (item != null)
            {
                return item;
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_destination_pickup_ != null && other.gameObject.GetHashCode() == m_destination_pickup_.GetHashCode())
            ComeBackBackToCamp();
    }

    private void ComeBackBackToCamp()
    {
        m_nav_mesh_agent.SetDestination(m_fire_camp.transform.position);
    }

    public void MoveToDestination()
    {
        if (m_destination_pickup_ != null)
            m_nav_mesh_agent.SetDestination(m_destination_pickup_.transform.position);
        m_pop_up_confirm.SetActive(false);
    }

    public void CancelMoveToDestination()
    {
        m_destination_pickup_ = null;
        m_pop_up_confirm.SetActive(false);
    }
}