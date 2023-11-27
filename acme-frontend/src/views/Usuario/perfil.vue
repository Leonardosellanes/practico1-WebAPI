<template>
    <div class=" w-full h-full space-y-10 ">
        <a-space direction="vertical" style="width: 100%; ">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Perfil de usuario">
                <template #extra>
                    <a-modal v-model:open="open" title="Nuevo Producto" :confirm-loading="confirmLoading" @ok="handleOk">
                        <a-space direction="vertical" style="width: 100%;">

                        </a-space>
                    </a-modal>
                </template>
            </a-page-header>
            <div class="w-full flex justify-between">
                    <a-card title="Tu informacion" :bordered="false" style="width: 500px">
                        <template #extra><a href="#">
                                <EditOutlined />
                                editar
                            </a>
                        </template>
                        <p>Card content</p>
                        <p>Card content</p>
                        <p>Card content</p>
                    </a-card>
                    <a-table :columns="columns" :data-source="data" class="w-full px-6">
                        <template #title>Tus ordenes de compra</template>
                        <template #headerCell="{ column }">
                            <template v-if="column.key === 'name'">
                                <span>
                                    <smile-outlined />
                                    Name
                                </span>
                            </template>
                        </template>

                        <template #bodyCell="{ column, record }">
                            <template v-if="column.key === 'name'">
                                <a>
                                    {{ record.name }}
                                </a>
                            </template>
                            <template v-else-if="column.key === 'tags'">
                                <span>
                                    <a-tag v-for="tag in record.tags" :key="tag"
                                        :color="tag === 'loser' ? 'volcano' : tag.length > 5 ? 'geekblue' : 'green'">
                                        {{ tag.toUpperCase() }}
                                    </a-tag>
                                </span>
                            </template>
                            <template v-else-if="column.key === 'action'">
                                <span>
                                    <a>Realizar reclamo / ver Reclamo</a>
                                    <a-divider type="vertical" />
                                    <a>Ver productos</a>
                                </span>
                            </template>
                        </template>
                    </a-table>
            
            </div>
        </a-space>

    </div>
</template>

<script setup>
import { EditOutlined } from '@ant-design/icons-vue';


const columns = [
    {
        title: 'Fecha',
        dataIndex: 'name',
        width: '10%'
    },
    {
        title: 'Medio de pago',
        dataIndex: 'age',
        width: '10%'
    },
    {
        title: 'Direccion',
        dataIndex: 'address',
        width: '10%'
    },
    {
        title: 'Fecha estimada de entrega',
        key: 'tags',
        dataIndex: 'tags',
        width: '10%'
    },
    {
        title: 'Total',
        key: 'tags',
        dataIndex: 'tags',
        width: '10%'
    },
    {
        title: 'Estado',
        key: 'tags',
        dataIndex: 'tags',
        width: '10%'
    },
    {
        title: 'Action',
        key: 'action',
        width: '10%'
    },
];
const data = [
    {
        key: '1',
        name: 'John Brown',
        age: 32,
        address: 'New York No. 1 Lake Park',
        tags: ['nice', 'developer'],
    },
    {
        key: '2',
        name: 'Jim Green',
        age: 42,
        address: 'London No. 1 Lake Park',
        tags: ['loser'],
    },
    {
        key: '3',
        name: 'Joe Black',
        age: 32,
        address: 'Sidney No. 1 Lake Park',
        tags: ['cool', 'teacher'],
    },
];
</script>