// Author(s): 
// Ashima Bahl, asbahl@cisco.com 
using System;
using System.Collections.Generic;

namespace Cisco.Runbook
{
    public enum erType

    {
        DISK,
        PNODE,
        NODE,
        CLUSTER,
        DATASTORE,
        VIRTNODE,
        VIRTCLUSTER,
        VIRTDATASTORE,
        VIRTMACHINE,
        PDISK,
        PDATASTORE,
        VIRTMACHINESNAPSHOT,
        FOLDER,
        RESOURCEPOOL,
        FILE,
        VIRTDATACENTER,
        REPLICATION_APPLIANCE,
        REPLICATION_JOB,
        IP_POOL,
        DP_VM_SNAPSHOT,
        DP_VMGROUP_SNAPSHOT,
        DP_VM,
        DP_VMGROUP,
        DP_VM_CONFIG,
        DP_VM_SNAPSHOT_POINT,
        CLUSTER_PAIR
    };

    public enum erIDType
    {
        VCMOID,
        VMBIOSUUID,
        VMDSPATH
    };

    public class EntityDetail
    {
        public string name;
        public erType type;
        public string id;
        public erIDType idtype;
        public string confignum;
    }

}
