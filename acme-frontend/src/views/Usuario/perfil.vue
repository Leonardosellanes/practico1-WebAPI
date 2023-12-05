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
                                    <a-input-password v-model:value="formPassword.password" />
                                </a-form-item>

                                <a-form-item label="Repetir Contraseña" name="Verify"
                                    :rules="[{ required: true, message: 'Repita la contraseña' }]">
                                    <a-input-password v-model:value="formPassword.Verify" />

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
                <div v-else-if="rol == 'MANAGER' || rol == 'EMPLEADO' && Reportes.length && productoReporte != null > 0"
                    class="w-full bg-white p-4 rounded-xl ml-4 space-y-4">
                    <div class="w-full flex items-center justify-between">
                        <p>Reportes</p>
                        <div class="space-x-2">
                            <a-button type="primary" @click="descargarCSV">
                                Csv
                            </a-button>
                            <a-button style="background-color: #f40f02; color: white;" @click="descargarPDF">
                                <FilePdfOutlined />
                                Pdf
                            </a-button>
                            <a-button style="background-color: #1d6f42; color: white;" @click="descargarExcel">
                                <FileExcelOutlined />
                                Excel
                            </a-button>
                            
                        </div>
                    </div>
                    
                    <a-card style="width: 100%">
                        <div class="flex justify-between">
                            <p>Generado en la ultima semana</p>
                            <p>${{ Reportes[0] }}</p>
                        </div>
                    </a-card>
                    <a-card style="width: 100%">
                        <div class="flex justify-between">
                            <p>Generado el mes pasado</p>
                            <p>${{ Reportes[1] }}</p>
                        </div>
                    </a-card>
                    <a-card style="width: 100%">
                        <div class="flex justify-between">
                            <p>Metodo de pago favorito</p>
                            <p>{{ Reportes[2] }}</p>
                        </div>
                    </a-card>
                    <a-card style="width: 100%">
                        <div class="flex justify-between">
                            <p>Producto mas vendido</p>
                            <p>{{ productoTitulo }}(Cantidad:{{ cantidadProducto }})</p>
                        </div>
                    </a-card>
                    <a-card style="width: 100%">
                        <div class="flex justify-between">
                            <p>Metodo de envio facorito</p>
                            <p>{{ Reportes[4] }}</p>
                        </div>
                    </a-card>
                </div>

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
import EmpresaController from '../../services/EmpresasController'
import { LoadingOutlined,FileExcelOutlined,FilePdfOutlined  } from '@ant-design/icons-vue';
import { message } from 'ant-design-vue';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';


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
const Reportes = ref([]);
const cantidadProducto = ref()
const productoReporte = ref(null)
const productoTitulo = ref('')
const cargarOrdenes = () => {
    loading.value = true
    if (rol.value == 'USER') {
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
    } else if (rol.value == 'MANAGER' || rol.value == 'EMPLEADO') {
        EmpresaController.getReportes(empresaId.value)
            .then((response) => {
                console.log(response.data[3])
                Reportes.value = response.data
                const string = response.data[3]
                const partes = string.split(':');
                productoTitulo.value = partes[0].trim();
                cantidadProducto.value = partes[1].trim();

                loading.value = false;
            })
            .catch((error) => {
                loading.value = false;
                console.error('Error al obtener la lista de prductos:', error);
            });
    }

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

const descargarExcel = () => {
  // Datos que deseas incluir en el archivo Excel (ajusta según tus necesidades)
  const datos = [
    ['Este Mes', 'Mes pasado', 'Metodo de pago preferido', 'Producto mas vendido', 'Medio de envio preferido'],
    [Reportes.value[0], Reportes.value[1], Reportes.value[2], productoTitulo.value+'('+ cantidadProducto.value +')', Reportes.value[4]],
  ];

  // Crear un objeto de libro de Excel
  const libro = XLSX.utils.book_new();
  const hoja = XLSX.utils.aoa_to_sheet(datos);

  // Agregar la hoja al libro
  XLSX.utils.book_append_sheet(libro, hoja, 'Hoja1');

  // Convertir el libro a un archivo binario
  const arrayBytes = XLSX.write(libro, { bookType: 'xlsx', type: 'array' });
    const archivoBinario = new Blob([arrayBytes], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

  // Crear un enlace de descarga y simular clic
  const enlaceDescarga = document.createElement('a');
  enlaceDescarga.href = window.URL.createObjectURL(archivoBinario);
  enlaceDescarga.download = 'Reporte_Ventas.xlsx';
  document.body.appendChild(enlaceDescarga);
  enlaceDescarga.click();
  document.body.removeChild(enlaceDescarga);
};

const descargarPDF = () => {
  // Datos que deseas incluir en el archivo PDF (ajusta según tus necesidades)
  const datos = [
    ['Este Mes', Reportes.value[0]],
    ['Mes pasado', Reportes.value[1]],
    ['Metodo de pago preferido', Reportes.value[2]],
    ['Producto mas vendido', `${productoTitulo.value} (${cantidadProducto.value})`],
    ['Medio de envio preferido', Reportes.value[4]],
  ];

  // Crear un objeto de documento PDF
  const pdf = new jsPDF();

  // Configurar estilos y posición
  const margen = 10;
  const espacioLinea = 12;
  const fontSizeTitulo = 16;
  const fontSizeCuerpo = 12;
  const colorTitulo = '#3498db';
  const colorCuerpo = '#333';

  // Agregar encabezado
  pdf.setFontSize(fontSizeTitulo);
  pdf.setTextColor(colorTitulo);
  pdf.text('Informe Mensual', margen, margen);

  // Agregar datos al documento
  pdf.setFontSize(fontSizeCuerpo);
  pdf.setTextColor(colorCuerpo);
  datos.forEach((fila, index) => {
    const x = margen;
    const y = margen + (index + 2) * espacioLinea;
    pdf.text(`${fila[0]}: ${fila[1]}`, x, y);
  });

  // Guardar o abrir el archivo
  pdf.save('informe_mensual.pdf');
};

function descargarCSV() {
  // Datos que deseas incluir en el archivo CSV (ajusta según tus necesidades)
  const datos = [
    ['Este Mes', Reportes.value[0]],
    ['Mes pasado', Reportes.value[1]],
    ['Metodo de pago preferido', Reportes.value[2]],
    ['Producto mas vendido', `${productoTitulo.value} (${cantidadProducto.value})`],
    ['Medio de envio preferido', Reportes.value[4]],
  ];

  // Convertir datos a formato CSV
  const csvContent = datos.map(row => row.join(',')).join('\n');

  // Crear un Blob con el contenido CSV
  const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });

  // Crear un enlace de descarga y simular clic
  const enlaceDescarga = document.createElement('a');
  enlaceDescarga.href = window.URL.createObjectURL(blob);
  enlaceDescarga.download = 'informe_mensual.csv';
  document.body.appendChild(enlaceDescarga);
  enlaceDescarga.click();
  document.body.removeChild(enlaceDescarga);
}

onMounted(() => {
    cargarInfoUser()
    cargarOrdenes();
});
</script>