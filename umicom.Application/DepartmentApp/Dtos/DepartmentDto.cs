using System;

namespace Umicom.Application.DepartmentApp.Dtos
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���ű��
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// ���Ÿ�����
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// ��������ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// �Ƿ���ɾ��
        /// </summary>
        public int IsDeleted { get; set; }
    }
}