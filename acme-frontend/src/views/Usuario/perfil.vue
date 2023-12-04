<template>
    <div class=" w-full h-full space-y-10 ">
        <a-space direction="vertical" style="width: 100%; ">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Perfil de usuario">
                <template #extra>
                    <a-modal v-model:open="open" title="Realizar reclamo" :confirm-loading="confirmLoading" @ok="reclamoOk">
                        <a-space direction="vertical" style="width: 100%;">
                            <a-textarea v-model:value="descripcionReclamo" :rows="4" placeholder="Ingrese su reclamo"
                                allow-clear />
                        </a-space>
                    </a-modal>
                    <a-modal v-model:open="openViewReclamo" title="Informacion de su reclamo" :footer="null">
                        <a-space direction="vertical" style="width: 100%;">
                            <a-card>
                                Fecha: {{ reclamoAsociado.value.fecha }} <br />
                            </a-card>
                            <a-card>
                                Descripcion: {{ reclamoAsociado.value.descripcion }} <br />
                            </a-card>
                        </a-space>
                    </a-modal>
                    <a-modal v-model:open="openViewProducto" title="Productos" :footer="null">
                        <a-space direction="vertical" style="width: 100%;">
                            <template v-for="product in productosAsociados.value">
                                <a-card style="width: 100%; margin-right: 16px;">
                                    <a-card-meta :title="product.pOs.titulo"
                                        :description="'$' + product.pOs.precio.toFixed(2)">
                                        <template #avatar>
                                            <a-avatar :size="64" :src="'data:image/png;base64,' + product.pOs.base64" />
                                        </template>

                                    </a-card-meta>
                                    <a-card style="margin-top: 8%;">
                                        Cantidad: {{ product.cantidad }}
                                    </a-card>
                                </a-card>
                            </template>
                        </a-space>
                    </a-modal>
                    <a-modal v-model:open="openEditarInfo" title="Editar mi informacion" :footer="null">
                        <a-space direction="vertical" style="width: 100%;">
                            <a-form :model="formEditar" name="basic" :label-col="{ span: 6 }" :wrapper-col="{ span: 16 }"
                                style="margin-top: 10%;" autocomplete="off" @finish="onFinish">
                                <a-form-item label="Nombre" name="name"
                                    :rules="[{ required: true, message: 'Ingrese el nombre' }]">
                                    <a-input v-model:value="formEditar.name" />
                                </a-form-item>

                                <a-form-item label="Apellido" name="lName"
                                    :rules="[{ required: true, message: 'Ingrese el apellido' }]">
                                    <a-input v-model:value="formEditar.lName" />
                                </a-form-item>

                                <a-form-item label="Direccion" name="address"
                                    :rules="[{ required: true, message: 'Ingrese una direccion' }]">
                                    <a-input v-model:value="formEditar.address" />
                                </a-form-item>

                                <a-form-item :wrapper-col="{ offset: 20, span: 16 }">
                                    <a-button type="primary" html-type="submit" :loading="confirmLoading">OK</a-button>
                                </a-form-item>
                            </a-form>
                        </a-space>
                    </a-modal>
                    <a-modal v-model:open="openPass" title="Cambiar contraseña" :footer="null">
                        <a-space direction="vertical" style="width: 100%;">
                            <a-form :model="formPassword" name="basic" :label-col="{ span: 8 }" :wrapper-col="{ span: 14 }"
                                style="margin-top: 10%;" autocomplete="off" @finish="onFinishPass">
                                <a-form-item label="Nueva Contraseña" name="password"
                                    :rules="[{ required: true, message: 'Ingrese la nueva contraseña' }]">
                                    <a-input-password  v-model:value="formPassword.password" />
                                </a-form-item>

                                <a-form-item label="Repetir Contraseña" name="Verify"
                                    :rules="[{ required: true, message: 'Repita la contraseña' }]">
                                    <a-input-password v-model:value="formPassword.Verify"/> 
                                
                                </a-form-item>

                                <a-form-item :wrapper-col="{ offset: 20, span: 16 }">
                                    <a-button type="primary" html-type="submit" :loading="confirmLoading">OK</a-button>
                                </a-form-item>
                            </a-form>
                        </a-space>
                    </a-modal>
                </template>
            </a-page-header>
            <div class="w-full flex justify-between">
                <a-card title="Tu informacion" :bordered="false" style="width: 500px">
                    <template #extra>
                        <a @click="openEditarInfo = true">
                            <EditOutlined />
                            editar
                        </a>
                        <a @click="openPass = true" class="ml-4">
                            <KeyOutlined />
                        </a>
                        
                    </template>
                    <div class="w-full flex justify-center" v-if="loading == true && infoUser != null">
                        <a-spin :indicator="indicator" @spinning="true" />
                    </div>
                    <div v-else>
                        <a-space direction="vertical">
                            <p v-if="infoUser != null">Nombre: {{ infoUser.name }}</p>
                            <p v-if="infoUser != null">Apellido: {{ infoUser.lName }}</p>
                            <p v-if="infoUser != null">Direccion: {{ infoUser.address }}</p>
                            <p v-if="infoUser != null">Email: {{ infoUser.email }}</p>
                        </a-space>
                    </div>
                </a-card>
                <a-table :columns="columns" :data-source="data" class="w-full px-6"
                    v-if="rol == 'USER' && loading == false">
                    <template #title>Tus ordenes de compra</template>
                    <template #bodyCell="{ column, record }">
                        <template v-if="column.key === 'action'">
                            <span>
                                <a @click="realizarReclamo(record)"
                                    v-if="record.rcs == null && record.estadoOrden == 'Entregado'">Realizar reclamo</a>
                                <a @click="verReclamo(record.rcs)" v-else-if="record.rcs != null">ver Reclamo</a>
                                <a-divider type="vertical" />
                                <a @click="verProductos(record.carritos)">Ver productos</a>
                            </span>
                        </template>
                    </template>
                </a-table>

            </div>
        </a-space>

    </div>
</template>

<script setup>
import { EditOutlined, KeyOutlined } from '@ant-design/icons-vue';
import { ref, onMounted, h, reactive } from 'vue';
import OrdenDeCompraController from '../../services/OrdenDeCompraController';
import ReclamosController from '../../services/ReclamosController'
import AuthController from '../../services/AuthController';
import { LoadingOutlined } from '@ant-design/icons-vue';
import { message } from 'ant-design-vue';

const idUsuario = ref(sessionStorage.getItem('idUsuario'));
const empresaId = ref(sessionStorage.getItem('empresaId'))
const rol = ref(sessionStorage.getItem('rol'))

const loading = ref(false);
const confirmLoading = ref(false);
const open = ref(false);
const openViewReclamo = ref(false);
const openViewProducto = ref(false);
const openEditarInfo = ref(false);
const openPass = ref(false);
const formEditar = reactive({
    id: '',
    name: '',
    lName: '',
    address: '',
    email: ''
})

const formPassword = reactive({
    password: '',
    Verify: ''
})
const descripcionReclamo = ref('');
const idOrden = ref();
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});
const columns = [
    {
        title: 'Fecha',
        dataIndex: 'fecha',
        width: '10%'
    },
    {
        title: 'Medio de pago',
        dataIndex: 'medioDePago',
        width: '10%'
    },
    {
        title: 'Direccion',
        dataIndex: 'direccionDeEnvio',
        width: '10%'
    },
    {
        title: 'Fecha estimada de entrega',
        dataIndex: 'fechaEstimadaEntrega',
        width: '10%'
    },
    {
        title: 'Total',
        dataIndex: 'total',
        width: '5%'
    },
    {
        title: 'Estado',
        dataIndex: 'estadoOrden',
        width: '10%'
    },
    {
        title: 'Action',
        key: 'action',
        width: '15%'
    },
];
const data = ref([]);
const reclamoAsociado = [];
const productosAsociados = ref([]);
const infoUser = ref(null);

const cargarOrdenes = () => {
    loading.value = true
    OrdenDeCompraController.getOrdenByUserId(idUsuario.value)
        .then((response) => {
            console.log(response)
            data.value = response.data.filter(item => item.estadoOrden != 'activo').reverse()
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la lista de prductos:', error);
        });
}

const cargarInfoUser = () => {
    loading.value = true
    AuthController.getInfoUser(idUsuario.value)
        .then((response) => {
            infoUser.value = response.data
            loading.value = false;
            console.log(infoUser.value)
            formEditar.name = response.data.name
            formEditar.lName = response.data.lName
            formEditar.address = response.data.address
            formEditar.email = response.data.email
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la informacion del usuario:', error);
        });
}

const realizarReclamo = (data) => {
    idOrden.value = data.id
    open.value = true
}

const verReclamo = (data) => {
    reclamoAsociado.value = data
    openViewReclamo.value = true
};

const verProductos = (data) => {
    productosAsociados.value = data
    openViewProducto.value = true
};

const reclamoOk = () => {
    confirmLoading.value = true

    const data = {
        id: 0,
        descripcion: descripcionReclamo.value,
        fecha: new Date(),
        empresaId: empresaId,
        ocId: idOrden.value
    }

    ReclamosController.createReclamo(data)
        .then(() => {
            confirmLoading.value = false;
            open.value = false
            cargarOrdenes()
        })
        .catch((error) => {
            confirmLoading.value = false;
            open.value = false
            console.error('Error al obtener la lista de prductos:', error);
        });
}

const onFinish = () => {
    console.log(formEditar.address)
    const data = {
        address: formEditar.address,
        name: formEditar.name,
        lName: formEditar.lName,
    }

    AuthController.editUser(idUsuario.value, data)
        .then(() => {
            message.success('informacion actualizada')
            cargarInfoUser()
            openEditarInfo.value = false
        })
        .catch((error) => {
            console.error('Error :', error);
            openEditarInfo.value = false
        });
};

const onFinishPass = () => {

    if (formPassword.password != formPassword.Verify) {
        message.error('Las constraseñas no coinciden')
        return
    }
    const data = {
        password: formPassword.password
    }

    AuthController.editUser(idUsuario.value, data)
        .then(() => {
            message.success('Contraseña actualizada')
            openPass.value = false
        })
        .catch((error) => {
            console.error('Error :', error);
            openPassInfo.value = false
        });
};

onMounted(() => {
    cargarInfoUser()
    cargarOrdenes();
});
</script>