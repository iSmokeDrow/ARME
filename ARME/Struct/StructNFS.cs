using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ARME.Struct
{
    /// <summary>
    /// Contains Important information about .NFS file
    /// </summary>
    public class NFS_HEADER
    {
        private string _signature;
        private int _version;
        private int _locationOffset;
        private int _monsterOffset;
        private int _npcOffset;
        private int _rowCount;

        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public int LocationOffset
        {
            get { return _locationOffset; }
            set { _locationOffset = value; }
        }

        public int MonsterOffset
        {
            get { return _monsterOffset; }
            set { _monsterOffset = value; }
        }

        public int NPCOffset
        {
            get { return _npcOffset; }
            set { _npcOffset = value; }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }
    }

    /// <summary>
    /// Contains information regarding Spawn Region
    /// e.g. Bounds and LUA call
    /// </summary>
    public class NFS_MONSTER_LOCATION
    {
        private int _regionIndex;
        private int _monsterCount;
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;
        private int _trigger;
        private int _scriptLength;
        private string _scriptText;

        /// <summary>
        /// Id of this spawn area
        /// </summary>
        public int RegionIndex
        {
            get { return _regionIndex; }
            set { _regionIndex = value; }
        }

        /// <summary>
        /// Amount of monsters being spawned (usually 1)
        /// </summary>
        public int MonsterCount
        {
            get { return _monsterCount; }
            set { _monsterCount = value; }
        }

        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        /// <summary>
        /// Trigger for spawn (always 1)
        /// </summary>
        public int Trigger
        {
            get { return _trigger; }
            set { _trigger = value; }
        }

        /// <summary>
        /// Spawn script length
        /// </summary>
        public int ScriptLength
        {
            get { return _scriptLength; }
            set { _scriptLength = value; }
        }

        /// <summary>
        /// Spawn script (lua link: monster_respawn.lua)
        /// </summary>
        public string ScriptText
        {
            get { return _scriptText; }
            set { _scriptText = value; }
        }
    }

    /// <summary>
    /// Container class for NPC spawn locations
    /// </summary>
    public class NpcLocation
    {
        private int _propId;
        private int _x;
        private int _y;
        private short _modelId;
        private int _scriptCount;

        /// <summary>
        /// Id of the npc
        /// </summary>
        public int NpcID
        {
            get { return _propId; }
            set { _propId = value; }
        }

        /// <summary>
        /// X coordinate for the npc
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Y coordinate for the npc
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Model_ID of the npc
        /// </summary>
        public short ModelID
        {
            get { return _modelId; }
            set { _modelId = value; }
        }

        /// <summary>
        /// Amount of scripts attached to the npc
        /// </summary>
        public int ScriptCount
        {
            get { return _scriptCount; }
            set { _scriptCount = value; }
        }
    }

    /// <summary>
    /// Container class for possible NPC Scripts
    /// </summary>
    public class NpcScripts
    {
        private int _npcID;
        private int _trigger;
        private int _scriptLength;
        private string _scriptText;
        private int _trigger_1;
        private int _scriptLength_1;
        private string _scriptText_1;

        /// <summary>
        /// Owner of the script
        /// </summary>
        public int NpcID
        {
            get { return _npcID; }
            set { _npcID = value; }
        }

        /// <summary>
        /// Trigger flag for this script
        /// </summary>
        public int Trigger
        {
            get { return _trigger; }
            set { _trigger = value; }
        }

        /// <summary>
        /// Script Length (used when reading ScriptText
        /// </summary>
        public int ScriptLength
        {
            get { return _scriptLength; }
            set { _scriptLength = value; }
        }

        /// <summary>
        /// Script Text (link to lua function)
        /// </summary>
        public string ScriptText
        {
            get { return _scriptText; }
            set { _scriptText = value; }
        }

        /// <summary>
        /// 2nd Trigger flag
        /// </summary>
        public int Trigger_1
        {
            get { return _trigger_1; }
            set { _trigger_1 = value; }
        }

        /// <summary>
        /// 2nd Script Length (used when reading ScriptText_1)
        /// </summary>
        public int ScriptLength_1
        {
            get { return _scriptLength_1; }
            set { _scriptLength_1 = value; }
        }

        /// <summary>
        /// 2nd Script Text (link to lua function)
        /// </summary>
        public string ScriptText_1
        {
            get { return _scriptText_1; }
            set { _scriptText_1 = value; }
        }
    }
}
