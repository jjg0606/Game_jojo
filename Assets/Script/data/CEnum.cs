namespace CEnum
{
    namespace Skill
    {
        /// <summary>
        /// Enum for SkillInterface State
        /// </summary>
        public enum State
        {
            /// <summary>
            /// 활성화 가능한 상태
            /// </summary>
            idle,
            /// <summary>
            /// 현재 활성화 된 상태
            /// </summary>
            ActiveNow,
            /// <summary>
            /// 활성화 불가능한 상태 (쿨다운 등의 이유로)
            /// </summary>
            Waiting         
        }

        /// <summary>
        /// 스킬의 인덱스를 변수로 나타내주는 클래스
        /// </summary>
        public static class Index
        {
            public static readonly int Q = 0;
            public static readonly int W = 1;
            public static readonly int E = 2;
            public static readonly int R = 3;
        }
    }




}
